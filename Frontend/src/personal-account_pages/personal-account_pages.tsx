import { useState } from 'react'
import './personal-account_pages.sass'

function personalAccount_pages() {
    const [count, setCount] = useState(0)

    return (
        <>
            <main className='main'>
                <div className="profile-container">
                    <h1 className="profile-title">Профиль</h1>

                    <div className="profile-menu-widget">
                        <div className="profile-menu-widget-item">
                            <img className='profile-menu-widget-item-img' src="/icons/orders.svg" alt="Заказы" />
                            <p className='profile-menu-widget-item-text'>Заказы</p>
                        </div>
                        <div className="profile-menu-widget-item">
                            <img className='profile-menu-widget-item-img' src="/icons/favorite.svg" alt="Избранное" />
                            <p className='profile-menu-widget-item-text'>Избранное</p>
                        </div>
                        <div className="profile-menu-widget-item">
                            <img className='profile-menu-widget-item-img' src="/icons/cart.svg" alt="Корзина" />
                            <p className='profile-menu-widget-item-text'>Корзина</p>
                        </div>
                        <div className="profile-menu-widget-item">
                            <img className='profile-menu-widget-item-img' src="/icons/location.svg" alt="Город" />
                            <p className='profile-menu-widget-item-text'>Новости</p>
                        </div>
                        <div className="profile-menu-widget-item">
                            <img className='profile-menu-widget-item-img' src="/icons/location.svg" alt="Город" />
                            <p className='profile-menu-widget-item-text'>г. Ульяновск</p>
                        </div>
                        <div className="profile-menu-widget-item">
                            <img className='profile-menu-widget-item-img' src="/icons/phone.svg" alt="Телефон" />
                            <p className='profile-menu-widget-item-text'>8-800-77-07-999</p>
                        </div>
                    </div>

                    <div className="user-content">
                        <div className="user-profile-card">
                            <div className="user-profile-header">
                                <div className="user-avatar">
                                    <img src="/icons/avatar.svg" alt="Аватар" />
                                </div>
                                <div className="user-basic-info">
                                    <p className="user-reg-date">Дата регистрации: 23.12.2023</p>
                                </div>
                            </div>

                            <div className="user-details">
                                <div className="user-details-row">
                                    <div className="user-detail-item">
                                        <label className="user-detail-label">Имя</label>
                                        <input
                                            type="text"
                                            className="user-detail-input"
                                            placeholder="Введите имя"
                                        />
                                    </div>
                                    <div className="user-detail-item">
                                        <label className="user-detail-label">Телефон</label>
                                        <div className="phone-input-wrapper">
                                            <span className="phone-prefix">+7</span>
                                            <input
                                                type="tel"
                                                className="user-detail-input phone-input"
                                                value="964 859-29-88"
                                                readOnly
                                            />
                                        </div>
                                    </div>
                                </div>

                                <div className="user-details-row">
                                    <div className="user-detail-item">
                                        <label className="user-detail-label">Фамилия</label>
                                        <input
                                            type="text"
                                            className="user-detail-input"
                                            placeholder="Введите фамилию"
                                        />
                                    </div>
                                    <div className="user-detail-item">
                                        <label className="user-detail-label">Электронная почта</label>
                                        <input
                                            type="email"
                                            className="user-detail-input"
                                            placeholder="Введите email"
                                        />
                                    </div>
                                </div>

                                <div className="user-details-row">
                                    <div className="user-detail-item">
                                        <div className="user-actions">
                                            <button className="action-btn logout-btn">Выйти</button>
                                            <button className="action-btn change-password-btn">Сменить пароль</button>
                                        </div>
                                    </div>
                                </div>

                                <div className="delete-profile-section">
                                    <button className="delete-profile-btn">Удалить профиль</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </main>
        </>
    )
}

export default personalAccount_pages
