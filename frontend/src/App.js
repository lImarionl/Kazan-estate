import React, { useState, useEffect } from 'react';
import './index.css';
import Auth from './Auth';
import Profile from './Profile';
import ComplexList from './ComplexList';
import { apiService } from './apiService';
import { ENDPOINTS } from './apiConfig';
import { useNotification } from './NotificationContext';

function App() {
  const [token, setToken] = useState(localStorage.getItem('jwtToken'));
  const [view, setView] = useState('home'); // 'home', 'complexes', 'profile'
  const { addNotification } = useNotification();

  const handleLogin = (newToken) => {
    localStorage.setItem('jwtToken', newToken);
    setToken(newToken);
  };

  const handleLogout = () => {
    localStorage.removeItem('jwtToken');
    setToken(null);
    setView('home');
    addNotification('Вы вышли из системы', 'success');
  };

  if (!token) {
    return <Auth onLogin={handleLogin} />;
  }

  const renderView = () => {
    switch (view) {
      case 'profile':
        return <Profile token={token} onBack={() => setView('home')} />;
      case 'complexes':
        return <ComplexList token={token} onBack={() => setView('home')} />;
      default:
        return (
          <div className="container">
            <header>
              <div className="logo">KAZAN ESTATE</div>
              <nav style={{ display: 'flex', gap: '1rem', alignItems: 'center' }}>
                <button onClick={() => setView('complexes')} style={{ background: 'transparent', border: '1px solid var(--glass-border)' }}>Новостройки</button>
                <button onClick={() => setView('profile')} style={{ background: 'transparent', border: '1px solid var(--glass-border)' }}>Профиль</button>
                <button onClick={handleLogout} style={{ background: '#ef4444' }}>Выход</button>
              </nav>
            </header>

            <section style={{ textAlign: 'center', padding: '5rem 0' }}>
              <h1 style={{ fontSize: '3.5rem', marginBottom: '1.5rem', background: 'linear-gradient(to right, #60a5fa, #a855f7)', WebkitBackgroundClip: 'text', WebkitTextFillColor: 'transparent' }}>
                Найдите идеальное жилье в Казани
              </h1>
              <p style={{ fontSize: '1.25rem', color: '#94a3b8', maxWidth: '800px', margin: '0 auto 3rem' }}>
                Интеллектуальная система подбора недвижимости и прогнозирования цен на основе нейронных сетей.
              </p>
              <div style={{ display: 'flex', gap: '1rem', justifyContent: 'center' }}>
                <button onClick={() => setView('complexes')} style={{ padding: '1rem 2rem', fontSize: '1.1rem' }}>Смотреть новостройки</button>
                 </div>
            </section>

            <footer style={{ marginTop: '5rem', padding: '2rem 0', borderTop: '1px solid rgba(255,255,255,0.1)', textAlign: 'center', color: '#64748b' }}>
              &copy; 2026 Kazan Estate Рекомендательная система.
            </footer>
          </div>
        );
    }
  };

  return renderView();
}

export default App;
