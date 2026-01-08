import React from 'react';
import './admin_panel_full.sass';

const AdminMain: React.FC = () => {
    return (
        <div className="admin-main">
            <div className="dashboard-stats">
                <div className="stat-card">
                    <h3>Всего пользователей</h3>
                    <div className="stat-value">1,248</div>
                    <div className="stat-change positive">
                        <span>↑</span>
                        <span>+12% за месяц</span>
                    </div>
                </div>
                <div className="stat-card">
                    <h3>Всего заказов</h3>
                    <div className="stat-value">356</div>
                    <div className="stat-change positive">
                        <span>↑</span>
                        <span>+8% за месяц</span>
                    </div>
                </div>
                <div className="stat-card">
                    <h3>Товаров в каталоге</h3>
                    <div className="stat-value">892</div>
                    <div className="stat-change positive">
                        <span>↑</span>
                        <span>+5% за месяц</span>
                    </div>
                </div>
                <div className="stat-card">
                    <h3>Общий доход</h3>
                    <div className="stat-value">₽4.2M</div>
                    <div className="stat-change positive">
                        <span>↑</span>
                        <span>+15% за месяц</span>
                    </div>
                </div>
            </div>

            <div className="admin-section">
                <div className="section-header">
                    <div>
                        <h2>Последняя активность</h2>
                        <p className="section-subtitle">Последние действия в системе</p>
                    </div>
                    <button className="btn-secondary">Смотреть все</button>
                </div>
                <div className="recent-activity">
                    <div className="activity-item">
                        <div className="activity-icon">👤</div>
                        <div className="activity-details">
                            <h4>Новый пользователь зарегистрирован</h4>
                            <p>user@example.com • Роль: Покупатель • ID: #1249</p>
                        </div>
                        <div className="activity-time">5 мин назад</div>
                    </div>
                    <div className="activity-item">
                        <div className="activity-icon">📦</div>
                        <div className="activity-details">
                            <h4>Новый заказ #ORD-78945</h4>
                            <p>Сумма: ₽80,799 • Статус: В обработке • Клиент: Иван Иванов</p>
                        </div>
                        <div className="activity-time">15 мин назад</div>
                    </div>
                    <div className="activity-item">
                        <div className="activity-icon">📰</div>
                        <div className="activity-details">
                            <h4>Опубликована новая новость</h4>
                            <p>«Обновление каталога техники» • Автор: Редактор • Просмотры: 1,245</p>
                        </div>
                        <div className="activity-time">1 час назад</div>
                    </div>
                    <div className="activity-item">
                        <div className="activity-icon">🛒</div>
                        <div className="activity-details">
                            <h4>Добавлен новый товар</h4>
                            <p>Ноутбук Apple MacBook Air • SKU: 88-999-P • Цена: ₽80,799</p>
                        </div>
                        <div className="activity-time">2 часа назад</div>
                    </div>
                    <div className="activity-item">
                        <div className="activity-icon">⚠️</div>
                        <div className="activity-details">
                            <h4>Требуется проверка заказа</h4>
                            <p>Заказ #ORD-78944 имеет спорные данные • Требуется подтверждение оплаты</p>
                        </div>
                        <div className="activity-time">3 часа назад</div>
                    </div>
                </div>
            </div>

            <div className="admin-section">
                <div className="section-header">
                    <div>
                        <h2>Быстрые действия</h2>
                        <p className="section-subtitle">Часто используемые функции</p>
                    </div>
                </div>
                <div style={{ display: 'flex', gap: '12px', flexWrap: 'wrap' }}>
                    <button className="btn-primary">📦 Добавить товар</button>
                    <button className="btn-primary">📰 Создать новость</button>
                    <button className="btn-primary">👥 Управление пользователями</button>
                    <button className="btn-primary">📊 Просмотреть статистику</button>
                    <button className="btn-secondary">🔄 Обновить каталог</button>
                    <button className="btn-secondary">📧 Отправить рассылку</button>
                </div>
            </div>

            <div className="admin-section">
                <div className="section-header">
                    <div>
                        <h2>Популярные товары</h2>
                        <p className="section-subtitle">Самые просматриваемые товары за неделю</p>
                    </div>
                    <button className="btn-secondary">Смотреть все</button>
                </div>
                <div className="product-grid">
                    <div className="product-card">
                        <div className="product-image">💻</div>
                        <div className="product-content">
                            <h3>Ноутбук Apple MacBook Air</h3>
                            <p className="product-sku">SKU: 88-999-P</p>
                            <p className="product-price">₽80,799</p>
                            <div className="product-actions">
                                <button className="btn-outline" style={{ flex: 1 }}>Редактировать</button>
                                <button className="btn-danger">Удалить</button>
                            </div>
                        </div>
                    </div>
                    <div className="product-card">
                        <div className="product-image">📱</div>
                        <div className="product-content">
                            <h3>iPhone 15 Pro Max 256GB</h3>
                            <p className="product-sku">SKU: 77-888-P</p>
                            <p className="product-price">₽125,990</p>
                            <div className="product-actions">
                                <button className="btn-outline" style={{ flex: 1 }}>Редактировать</button>
                                <button className="btn-danger">Удалить</button>
                            </div>
                        </div>
                    </div>
                    <div className="product-card">
                        <div className="product-image">⌚️</div>
                        <div className="product-content">
                            <h3>Apple Watch Series 9</h3>
                            <p className="product-sku">SKU: 66-777-P</p>
                            <p className="product-price">₽45,990</p>
                            <div className="product-actions">
                                <button className="btn-outline" style={{ flex: 1 }}>Редактировать</button>
                                <button className="btn-danger">Удалить</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default AdminMain;