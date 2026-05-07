import React, { useState } from 'react';
import { apiService } from './apiService';
import { ENDPOINTS } from './apiConfig';
import { useNotification } from './NotificationContext';

function Auth({ onLogin }) {
  const [isRegister, setIsRegister] = useState(false);
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [email, setEmail] = useState('');
  const { addNotification } = useNotification();

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    if (isRegister) {
      const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      if (!emailRegex.test(email)) {
        addNotification('Введите корректный адрес электронной почты (Email)', 'error');
        return;
      }
    }

    const url = isRegister ? ENDPOINTS.AUTH.REGISTER : ENDPOINTS.AUTH.LOGIN;
    const body = isRegister ? { username, password, email } : { username, password };

    try {
      const data = await apiService.post(url, body);
      if (isRegister) {
        addNotification('Регистрация успешна! Теперь войдите.', 'success');
        setIsRegister(false);
      } else {
        addNotification('Добро пожаловать!', 'success');
        onLogin(data.token);
      }
    } catch (err) {
      addNotification(`Ошибка: ${err.message}`, 'error');
    }
  };

  return (
    <div className="card" style={{ maxWidth: '400px', margin: '100px auto' }}>
      <h2 style={{ textAlign: 'center', marginBottom: '2rem' }}>{isRegister ? 'Регистрация' : 'Вход'}</h2>
      <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: '1rem' }}>
        <input 
          type="text" 
          placeholder="Логин" 
          value={username} 
          onChange={(e) => setUsername(e.target.value)} 
          required 
        />
        {isRegister && (
          <input 
            type="email" 
            placeholder="Email" 
            value={email} 
            onChange={(e) => setEmail(e.target.value)} 
            required 
          />
        )}
        <input 
          type="password" 
          placeholder="Пароль" 
          value={password} 
          onChange={(e) => setPassword(e.target.value)} 
          required 
        />
        <button type="submit">{isRegister ? 'Зарегистрироваться' : 'Войти'}</button>
      </form>
      <p style={{ textAlign: 'center', marginTop: '1rem', cursor: 'pointer', color: '#94a3b8' }} 
         onClick={() => setIsRegister(!isRegister)}>
        {isRegister ? 'Уже есть аккаунт? Войти' : 'Нет аккаунта? Регистрация'}
      </p>
    </div>
  );
}

export default Auth;
