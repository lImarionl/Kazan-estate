import React, { useState, useEffect } from 'react';
import { apiService } from './apiService';
import { ENDPOINTS } from './apiConfig';
import { useNotification } from './NotificationContext';

function Profile({ token, onBack }) {
  const [profile, setProfile] = useState({ fullName: '', email: '', phoneNumber: '', username: '' });
  const [isEditing, setIsEditing] = useState(false);
  const [loading, setLoading] = useState(true);
  const { addNotification } = useNotification();

  useEffect(() => {
    apiService.get(ENDPOINTS.USER.PROFILE, token)
      .then(data => {
        setProfile(data);
        setLoading(false);
      })
      .catch(err => {
        addNotification('Не удалось загрузить профиль', 'error');
        console.error(err);
      });
  }, [token, addNotification]);

  const handleUpdate = async (e) => {
    e.preventDefault();
    
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (profile.email && !emailRegex.test(profile.email)) {
      addNotification('Введите корректный адрес электронной почты (Email)', 'error');
      return;
    }

    if (profile.phoneNumber) {
      const phoneRegex = /^(\+7|7|8)?[\s\-]?\(?[49][0-9]{2}\)?[\s\-]?[0-9]{3}[\s\-]?[0-9]{2}[\s\-]?[0-9]{2}$/;
      if (!phoneRegex.test(profile.phoneNumber)) {
        addNotification('Некорректный формат телефона. Используйте +7 (999) 999-99-99 или 89999999999', 'error');
        return;
      }
    }

    try {
      await apiService.put(ENDPOINTS.USER.PROFILE, {
        email: profile.email,
        fullName: profile.fullName,
        phoneNumber: profile.phoneNumber
      }, token);
      
      addNotification('Профиль успешно обновлен!', 'success');
      setIsEditing(false);
    } catch (err) {
      addNotification(`Ошибка при обновлении: ${err.message}`, 'error');
    }
  };

  if (loading) return <div className="container">Загрузка...</div>;

  return (
    <div className="container">
      <button onClick={onBack} style={{ marginBottom: '2rem', background: 'transparent', border: '1px solid var(--glass-border)' }}>&larr; Назад</button>
      
      <div className="card" style={{ maxWidth: '600px', margin: '0 auto' }}>
        <h2 style={{ marginBottom: '2rem' }}>Профиль пользователя: {profile.username}</h2>
        
        <form onSubmit={handleUpdate} style={{ display: 'flex', flexDirection: 'column', gap: '1.5rem' }}>
          <div style={{ display: 'flex', flexDirection: 'column', gap: '0.5rem' }}>
            <label>ФИО</label>
            <input 
              type="text" 
              value={profile.fullName} 
              onChange={e => setProfile({...profile, fullName: e.target.value})} 
              disabled={!isEditing}
              placeholder="Введите ваше имя"
            />
          </div>

          <div style={{ display: 'flex', flexDirection: 'column', gap: '0.5rem' }}>
            <label>Email</label>
            <input 
              type="email" 
              value={profile.email} 
              onChange={e => setProfile({...profile, email: e.target.value})} 
              disabled={!isEditing}
            />
          </div>

          <div style={{ display: 'flex', flexDirection: 'column', gap: '0.5rem' }}>
            <label>Телефон</label>
            <input 
              type="text" 
              value={profile.phoneNumber} 
              onChange={e => setProfile({...profile, phoneNumber: e.target.value})} 
              disabled={!isEditing}
              placeholder="+7 (___) ___-__-__"
            />
          </div>

          {isEditing ? (
            <div style={{ display: 'flex', gap: '1rem' }}>
              <button type="submit">Сохранить изменения</button>
              <button type="button" onClick={() => setIsEditing(false)} style={{ background: '#4b5563' }}>Отмена</button>
            </div>
          ) : (
            <button type="button" onClick={() => setIsEditing(true)}>Редактировать профиль</button>
          )}
        </form>
      </div>
    </div>
  );
}

export default Profile;
