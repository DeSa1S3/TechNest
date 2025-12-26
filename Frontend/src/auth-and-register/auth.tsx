import { useState } from 'react'
import './auth.sass'
import { Link } from 'react-router-dom'

function AuthAndRegister() {
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [rememberMe, setRememberMe] = useState(false)

    const handleLogin = () => {
        console.log('Вход с email:', email, 'и паролем:', password)
        console.log('Запомнить меня:', rememberMe)
    }

    const handleRegister = () => {
        console.log('Переход к регистрации')
        window.location.href = '/register'
    }

    const handleForgotPassword = () => {
        console.log('Восстановление пароля для:', email)
        window.location.href = '/forgot-password'
    }

    return (
        <div className="auth-container">
            <div className="auth-card">
                <h1 className="auth-title">Войти</h1>

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
                        />
                    </div>

                    <div className="input-group">
                        <div className="password-header">
                            <label htmlFor="password">Пароль</label>
                            <span
                                className="forgot-password-link"
                                onClick={handleForgotPassword}
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
                        />
                    </div>

                    <div className="remember-me">
                        <label className="checkbox-label">
                            <input
                                type="checkbox"
                                checked={rememberMe}
                                onChange={(e) => setRememberMe(e.target.checked)}
                                className="checkbox-input"
                            />
                            <span className="checkbox-custom"></span>
                            <span className="checkbox-text">Запомнить меня</span>
                        </label>
                    </div>

                    <button
                        className="login-btn"
                        onClick={handleLogin}
                    >
                        Войти
                    </button>

                    <div className="register-section">
                        <p className="register-text">
                            Нет аккаунта?
                            <Link to="/auth">
                                <span
                                    className="register-link"
                                    onClick={handleRegister}
                                >
                                    Зарегистрироваться
                                </span>
                            </Link>

                        </p>
                    </div>

                    <p className="consent-text">
                        Нажимая кнопку «Войти», вы даёте согласие на обработку своих персональных данных в соответствии с <a href="#" className="policy-link">Политикой в отношении обработки персональных данных</a>.
                    </p>
                </div>
            </div>
        </div>
    )
}

export default AuthAndRegister