import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { authService } from '../service/apiServices';
import './auth.sass';

const AuthAndRegister: React.FC = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [rememberMe, setRememberMe] = useState(false);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const navigate = useNavigate();

    const handleLogin = async () => {
        if (!email || !password) {
            setError('Пожалуйста, заполните все поля');
            return;
        }

        setIsLoading(true);
        setError(null);

        try {
            const response = await authService.login(email, password);

            if (response.accessToken) {
                localStorage.setItem('user_email', email);

                navigate('/admin');
            } else {
                setError('Неверный email или пароль');
            }
        } catch (err: any) {
            setError(err.message || 'Ошибка при входе');
        } finally {
            setIsLoading(false);
        }
    };

    const handleRegister = () => {
        navigate('/register');
    };

    const handleForgotPassword = () => {
        navigate('/forgot-password');
    };

    return (
        <div className="auth-container">
            <div className="auth-card">
                <h1 className="auth-title">Войти</h1>

                {error && (
                    <div className="auth-error">
                        {error}
                    </div>
                )}

                <div className="auth-form">
                    <div className="input-group">
                        <label htmlFor="email">E-mail</label>
                        <input
                            id="email"
                            type="email"
                            placeholder="example@mail.com"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            className="auth-input"
                            disabled={isLoading}
                        />
                    </div>

                    <div className="input-group">
                        <div className="password-header">
                            <label htmlFor="password">Пароль</label>
                            <span
                                className="forgot-password-link"
                                onClick={!isLoading ? handleForgotPassword : undefined}
                            >
                                Забыли пароль?
                            </span>
                        </div>
                        <input
                            id="password"
                            type="password"
                            placeholder="Введите ваш пароль"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            className="auth-input"
                            disabled={isLoading}
                        />
                    </div>

                    <div className="remember-me">
                        <label className="checkbox-label">
                            <input
                                type="checkbox"
                                checked={rememberMe}
                                onChange={(e) => setRememberMe(e.target.checked)}
                                className="checkbox-input"
                                disabled={isLoading}
                            />
                            <span className="checkbox-custom"></span>
                            <span className="checkbox-text">Запомнить меня</span>
                        </label>
                    </div>

                    <button
                        className="login-btn"
                        onClick={handleLogin}
                        disabled={isLoading}
                    >
                        {isLoading ? 'Загрузка...' : 'Войти'}
                    </button>

                    <div className="register-section">
                        <p className="register-text">
                            Нет аккаунта?
                            <span
                                className="register-link"
                                onClick={!isLoading ? handleRegister : undefined}
                            >
                                Зарегистрироваться
                            </span>
                        </p>
                    </div>

                    <p className="consent-text">
                        Нажимая кнопку «Войти», вы даёте согласие на обработку своих персональных данных в соответствии с <a href="/privacy" className="policy-link">Политикой в отношении обработки персональных данных</a>.
                    </p>
                </div>
            </div>
        </div>
    );
};

export default AuthAndRegister;