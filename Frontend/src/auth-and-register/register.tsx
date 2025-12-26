import { useState } from 'react'
import './register.sass'
import { Link } from 'react-router-dom'

function Register() {
    const [firstName, setFirstName] = useState('')
    const [lastName, setLastName] = useState('')
    const [birthDate, setBirthDate] = useState('')
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [confirmPassword, setConfirmPassword] = useState('')
    const [agreeToTerms, setAgreeToTerms] = useState(false)

    const handleRegister = () => {
        if (password !== confirmPassword) {
            alert('Пароли не совпадают!')
            return
        }
        console.log('Регистрация:', {
            firstName,
            lastName,
            birthDate,
            email,
            password
        })
    }

    const handleLogin = () => {
        console.log('Переход ко входу')
        window.location.href = '/auth'
    }

    return (
        <div className="register-container">
            <div className="register-card">
                <h1 className="register-title">Регистрация</h1>

                <div className="register-form">
                    <div className="name-group">
                        <div className="input-group half-width">
                            <label htmlFor="firstName">Имя</label>
                            <input
                                id="firstName"
                                type="text"
                                placeholder="Иван"
                                value={firstName}
                                onChange={(e) => setFirstName(e.target.value)}
                                className="register-input"
                            />
                        </div>

                        <div className="input-group half-width">
                            <label htmlFor="lastName">Фамилия</label>
                            <input
                                id="lastName"
                                type="text"
                                placeholder="Иванов"
                                value={lastName}
                                onChange={(e) => setLastName(e.target.value)}
                                className="register-input"
                            />
                        </div>
                    </div>

                    <div className="input-group">
                        <label htmlFor="birthDate">Дата рождения</label>
                        <input
                            id="birthDate"
                            type="date"
                            value={birthDate}
                            onChange={(e) => setBirthDate(e.target.value)}
                            className="register-input"
                        />
                    </div>

                    <div className="input-group">
                        <label htmlFor="email">E-mail</label>
                        <input
                            id="email"
                            type="email"
                            placeholder="example@mail.com"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            className="register-input"
                        />
                    </div>

                    <div className="input-group">
                        <label htmlFor="password">Пароль</label>
                        <input
                            id="password"
                            type="password"
                            placeholder="Придумайте пароль"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            className="register-input"
                        />
                    </div>

                    <div className="input-group">
                        <label htmlFor="confirmPassword">Подтверждение пароля</label>
                        <input
                            id="confirmPassword"
                            type="password"
                            placeholder="Повторите пароль"
                            value={confirmPassword}
                            onChange={(e) => setConfirmPassword(e.target.value)}
                            className="register-input"
                        />
                    </div>

                    <div className="terms-agreement">
                        <label className="checkbox-label">
                            <input
                                type="checkbox"
                                checked={agreeToTerms}
                                onChange={(e) => setAgreeToTerms(e.target.checked)}
                                className="checkbox-input"
                            />
                            <span className="checkbox-custom"></span>
                            <span className="checkbox-text">
                                Я принимаю условия <a href="#" className="terms-link">Пользовательского соглашения</a> и даю согласие на обработку персональных данных
                            </span>
                        </label>
                    </div>

                    <button
                        className="register-btn"
                        onClick={handleRegister}
                        disabled={!agreeToTerms}
                    >
                        Зарегистрироваться
                    </button>

                    <div className="login-section">
                        <p className="login-text">
                            Уже есть аккаунт?
                            <Link to="/auth">
                                <span
                                    className="login-link"
                                    onClick={handleLogin}
                                >
                                    Войти
                                </span>
                            </Link>

                        </p>
                    </div>

                    <p className="consent-text">
                        Регистрируясь, вы соглашаетесь с <a href="#" className="policy-link">Политикой конфиденциальности</a> и даёте согласие на обработку персональных данных.
                    </p>
                </div>
            </div>
        </div>
    )
}

export default Register