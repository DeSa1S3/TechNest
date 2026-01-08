import React, { useState } from 'react';
import AdminMain from '../admin_panel_full/admin_panel_full';
import AdminUsers from '../admin_panel_user/admin_panel_user';
import AdminCatalogItem from '../admin_panel_catalog_item/admin_panel_catalog_item';
import AdminShopItem from '../admin_panel_shop_item/admin_panel_shop_item';
import AdminNews from '../admin_panel_news/admin_panel_news';
import AdminOrders from '../admin_panel_order/admin_panel_order';
import './admin_panel_main.sass';

const AdminPanel: React.FC = () => {
    const [activeTab, setActiveTab] = useState<'main' | 'users' | 'catalog' | 'shop' | 'news' | 'orders'>('main');

    return (
        <div className="admin-panel">
            <header className="admin-header">
                <div className="admin-header-top">
                    <div className="admin-title-section">
                        <h1 className="admin-title"># TechNest</h1>
                        <p className="admin-subtitle">Административная панель</p>
                    </div>
                    <div className="admin-header-actions">
                        <div className="admin-search">
                            <input type="text" placeholder="Поиск по сайту" />
                        </div>
                        <div className="admin-user-menu">
                            <span>Администратор</span>
                            <div className="user-avatar">A</div>
                        </div>
                    </div>
                </div>

                <nav className="admin-nav">
                    <button
                        className={`admin-nav-btn ${activeTab === 'main' ? 'active' : ''}`}
                        onClick={() => setActiveTab('main')}
                    >
                        📊 Главная
                    </button>
                    <button
                        className={`admin-nav-btn ${activeTab === 'users' ? 'active' : ''}`}
                        onClick={() => setActiveTab('users')}
                    >
                        👥 Пользователи
                    </button>
                    <button
                        className={`admin-nav-btn ${activeTab === 'catalog' ? 'active' : ''}`}
                        onClick={() => setActiveTab('catalog')}
                    >
                        📁 Каталог
                    </button>
                    <button
                        className={`admin-nav-btn ${activeTab === 'shop' ? 'active' : ''}`}
                        onClick={() => setActiveTab('shop')}
                    >
                        🛒 Продукция
                    </button>
                    <button
                        className={`admin-nav-btn ${activeTab === 'news' ? 'active' : ''}`}
                        onClick={() => setActiveTab('news')}
                    >
                        📰 Новости
                    </button>
                    <button
                        className={`admin-nav-btn ${activeTab === 'orders' ? 'active' : ''}`}
                        onClick={() => setActiveTab('orders')}
                    >
                        📦 Заказы
                    </button>
                </nav>
            </header>

            <main className="admin-content">
                {activeTab === 'main' && <AdminMain />}
                {activeTab === 'users' && <AdminUsers />}
                {activeTab === 'catalog' && <AdminCatalogItem />}
                {activeTab === 'shop' && <AdminShopItem />}
                {activeTab === 'news' && <AdminNews />}
                {activeTab === 'orders' && <AdminOrders />}
            </main>
        </div>
    );
};

export default AdminPanel;