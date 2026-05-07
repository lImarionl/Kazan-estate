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
    const results = complexes.filter(c => 
      c.name.toLowerCase().includes(search.toLowerCase()) &&
      (districtFilter === '' || c.district.includes(districtFilter))
    );
    setFiltered(results);
  }, [search, districtFilter, complexes]);

  if (loading) return <div className="container">Загрузка данных...</div>;

  const districts = [...new Set(complexes.map(c => c.district))];

  return (
    <div className="container">
      <button onClick={onBack} style={{ marginBottom: '2rem', background: 'transparent', border: '1px solid var(--glass-border)' }}>&larr; Назад</button>
      <h1 style={{ marginBottom: '2rem' }}>Новостройки Казани</h1>

      <div className="card" style={{ marginBottom: '3rem', display: 'flex', gap: '1rem', alignItems: 'center' }}>
        <input 
          type="text" 
          placeholder="Поиск по названию..." 
          value={search}
          onChange={e => setSearch(e.target.value)}
          style={{ flex: 2 }}
        />
        <select 
          value={districtFilter} 
          onChange={e => setDistrictFilter(e.target.value)}
          style={{ flex: 1 }}
        >
          <option value="">Все районы</option>
          {districts.map(d => <option key={d} value={d}>{d}</option>)}
        </select>
        <button style={{ flex: 0.5 }}>Найти</button>
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
              <p style={{ fontSize: '0.85rem', color: '#94a3b8', marginBottom: '1rem', height: '3rem', overflow: 'hidden' }}>
                {item.description}
              </p>
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
