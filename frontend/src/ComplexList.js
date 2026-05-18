import React, { useState, useEffect } from 'react';
import { apiService } from './apiService';
import { ENDPOINTS } from './apiConfig';
import { useNotification } from './NotificationContext';

function ComplexList({ token, onBack }) {
  const [complexes, setComplexes] = useState([]);
  const [filtered, setFiltered] = useState([]);
  const [loading, setLoading] = useState(true);
  const [search, setSearch] = useState('');
  const [districtFilter, setDistrictFilter] = useState('');
  const [maxPrice, setMaxPrice] = useState('');
  const [aiRecommendations, setAiRecommendations] = useState(null);
  const [isAiLoading, setIsAiLoading] = useState(false);
  const { addNotification } = useNotification();

  useEffect(() => {
    apiService.get(ENDPOINTS.PROPERTY.COMPLEXES, token)
      .then(data => {
        setComplexes(data);
        setFiltered(data);
        setLoading(false);
      })
      .catch(err => {
        addNotification('Ошибка загрузки новостроек', 'error');
        setLoading(false);
      });
  }, [token, addNotification]);

  useEffect(() => {
    let results = complexes.filter(c => 
      c.name.toLowerCase().includes(search.toLowerCase())
    );
    
    if (aiRecommendations) {
      results = results.map(c => {
        const rec = aiRecommendations.find(r => r.complex_id === c.id);
        return { ...c, aiScore: rec ? rec.score : 0, aiReasoning: rec ? rec.reasoning : '' };
      }).sort((a, b) => b.aiScore - a.aiScore);
    } else {
      if (districtFilter !== '') {
        results = results.filter(c => c.district.includes(districtFilter));
      }
    }
    
    setFiltered(results);
  }, [search, districtFilter, complexes, aiRecommendations]);

  const handleAIPredict = async () => {
    if (!districtFilter || !maxPrice) {
      addNotification('Укажите район и бюджет для ИИ', 'warning');
      return;
    }
    
    setIsAiLoading(true);
    try {
      const response = await fetch('http://localhost:8000/predict', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          user_id: 1,
          preferred_districts: [districtFilter],
          max_price: parseFloat(maxPrice),
          complexes: complexes
        })
      });
      
      const data = await response.json();
      setAiRecommendations(data.recommendations);
      addNotification('ИИ успешно подобрал варианты!', 'success');
    } catch (err) {
      addNotification('Ошибка при запросе к ИИ', 'error');
    } finally {
      setIsAiLoading(false);
    }
  };

  if (loading) return <div className="container">Загрузка данных...</div>;

  const districts = [...new Set(complexes.map(c => c.district))];

  return (
    <div className="container">
      <button onClick={onBack} style={{ marginBottom: '2rem', background: 'transparent', border: '1px solid var(--glass-border)' }}>&larr; Назад</button>
      <h1 style={{ marginBottom: '2rem' }}>Новостройки Казани</h1>

      <div className="card" style={{ marginBottom: '2rem', display: 'flex', gap: '1rem', alignItems: 'center', flexWrap: 'wrap' }}>
        <h3 style={{ width: '100%', marginBottom: '0.5rem' }}>Умный подбор с ИИ</h3>
        <select 
          value={districtFilter} 
          onChange={e => setDistrictFilter(e.target.value)}
          style={{ flex: 1, minWidth: '200px' }}
        >
          <option value="">Выберите желаемый район</option>
          {districts.map(d => <option key={d} value={d}>{d}</option>)}
        </select>
        <input 
          type="number" 
          placeholder="Максимальный бюджет (₽)" 
          value={maxPrice}
          onChange={e => setMaxPrice(e.target.value)}
          style={{ flex: 1, minWidth: '200px' }}
        />
        <button 
          onClick={handleAIPredict} 
          disabled={isAiLoading || !districtFilter || !maxPrice}
          style={{ flex: 1, background: 'linear-gradient(45deg, #10b981, #059669)', border: 'none', minWidth: '200px' }}
        >
          {isAiLoading ? 'Анализ...' : 'Подобрать с ИИ'}
        </button>
      </div>

      <div className="card" style={{ marginBottom: '3rem', display: 'flex', gap: '1rem', alignItems: 'center' }}>
        <input 
          type="text" 
          placeholder="Поиск по названию..." 
          value={search}
          onChange={e => setSearch(e.target.value)}
          style={{ flex: 2 }}
        />
        <button onClick={() => { setSearch(''); setAiRecommendations(null); }} style={{ flex: 0.5, background: '#334155' }}>Сбросить</button>
      </div>

      <div className="grid">
        {filtered.map(item => (
          <div key={item.id} className="card" style={{ padding: 0, overflow: 'hidden' }}>
            <div style={{ height: '200px', width: '100%', overflow: 'hidden' }}>
              <img 
                src={item.imageUrl || 'https://via.placeholder.com/400x200?text=Kazan+Estate'} 
                alt={item.name} 
                style={{ width: '100%', height: '100%', objectFit: 'cover' }}
              />
            </div>
            <div style={{ padding: '1.5rem' }}>
              <span className={`badge ${item.class?.includes('Эко') ? 'badge-eco' : item.class?.includes('Бизнес') ? 'badge-business' : 'badge-comfort'}`}>
                {item.class}
              </span>
              <h2 style={{ fontSize: '1.25rem' }}>{item.name}</h2>
              <p className="district" style={{ marginBottom: '0.5rem' }}>{item.district}</p>
              
              {item.aiScore > 0 ? (
                <div style={{ marginBottom: '1rem', padding: '0.5rem', background: 'rgba(16, 185, 129, 0.1)', borderRadius: '8px', border: '1px solid rgba(16, 185, 129, 0.3)' }}>
                  <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: '0.5rem' }}>
                    <span style={{ fontWeight: 'bold', color: '#10b981' }}>Предрасположенность:</span>
                    <span style={{ fontWeight: 'bold', color: '#10b981' }}>{item.aiScore}%</span>
                  </div>
                  <p style={{ fontSize: '0.8rem', color: '#cbd5e1', margin: 0 }}>{item.aiReasoning}</p>
                </div>
              ) : (
                <p style={{ fontSize: '0.85rem', color: '#94a3b8', marginBottom: '1rem', height: '3rem', overflow: 'hidden' }}>
                  {item.description}
                </p>
              )}
              <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', borderTop: '1px solid var(--glass-border)', paddingTop: '1rem' }}>
                <div className="price-range">
                  от {item.minPrice?.toLocaleString()} ₽
                </div>
                <div style={{ fontSize: '0.75rem', color: '#64748b' }}>
                  Срок: {item.completionDate ? new Date(item.completionDate).getFullYear() : 'Не указан'}
                </div>
              </div>
            </div>
          </div>
        ))}
      </div>
      
      {filtered.length === 0 && (
        <div style={{ textAlign: 'center', marginTop: '5rem', color: '#94a3b8' }}>
          По вашему запросу ничего не найдено.
        </div>
      )}
    </div>
  );
}

export default ComplexList;
