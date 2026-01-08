import React, { useState } from 'react';
import type { Order } from '../../components/panel_index';
import './admin_panel_order.sass';

const AdminOrders: React.FC = () => {
    const [orders, setOrders] = useState<Order[]>([
        {
            id: 78945,
            userId: 3,
            userName: 'Иван Иванов',
            userEmail: 'ivanov@mail.ru',
            items: [
                { productId: 1, productName: 'Ноутбук Apple MacBook Air', quantity: 1, price: 80799, sku: '88-999-P' },
                { productId: 2, productName: 'Чехол для MacBook Air', quantity: 1, price: 2990, sku: 'ACC-001' }
            ],
            total: 83789,
            status: 'processing',
            createdAt: '2024-03-15 14:30',
            address: 'Москва, ул. Тверская, д. 1, кв. 12'
        },
        {
            id: 78944,
            userId: 4,
            userName: 'Петр Петров',
            userEmail: 'petrov@gmail.com',
            items: [
                { productId: 2, productName: 'iPhone 15 Pro Max 256GB', quantity: 1, price: 125990, sku: '77-888-P' },
                { productId: 3, productName: 'Apple Watch Series 9', quantity: 1, price: 45990, sku: '66-777-P' }
            ],
            total: 171980,
            status: 'pending',
            createdAt: '2024-03-14 11:15',
            address: 'Санкт-Петербург, Невский пр., д. 25'
        },
        {
            id: 78943,
            userId: 5,
            userName: 'Анна Сидорова',
            userEmail: 'sidorova@yandex.ru',
            items: [
                { productId: 1, productName: 'Ноутбук Apple MacBook Air', quantity: 1, price: 80799, sku: '88-999-P' },
                { productId: 4, productName: 'Наушники AirPods Pro', quantity: 1, price: 24990, sku: '55-666-P' }
            ],
            total: 105789,
            status: 'completed',
            createdAt: '2024-03-12 09:45',
            address: 'Екатеринбург, ул. Ленина, д. 45, офис 3'
        },
        {
            id: 78942,
            userId: 6,
            userName: 'Сергей Кузнецов',
            userEmail: 'kuznetsov@gmail.com',
            items: [
                { productId: 5, productName: 'iPad Pro 12.9" M2', quantity: 1, price: 139990, sku: '44-555-P' }
            ],
            total: 139990,
            status: 'cancelled',
            createdAt: '2024-03-10 16:20',
            address: 'Новосибирск, Красный пр., д. 100'
        },
        {
            id: 78941,
            userId: 7,
            userName: 'Мария Попова',
            userEmail: 'popova@mail.ru',
            items: [
                { productId: 2, productName: 'iPhone 15 Pro Max 256GB', quantity: 2, price: 125990, sku: '77-888-P' },
                { productId: 3, productName: 'Apple Watch Series 9', quantity: 2, price: 45990, sku: '66-777-P' }
            ],
            total: 343960,
            status: 'processing',
            createdAt: '2024-03-08 13:10',
            address: 'Казань, ул. Баумана, д. 15'
        },
    ]);

    const [selectedOrder, setSelectedOrder] = useState<Order | null>(null);
    const [showDetailsModal, setShowDetailsModal] = useState(false);
    const [statusFilter, setStatusFilter] = useState<string>('all');
    const [searchTerm, setSearchTerm] = useState('');

    const handleViewDetails = (order: Order) => {
        setSelectedOrder(order);
        setShowDetailsModal(true);
    };

    const handleUpdateStatus = (orderId: number, newStatus: Order['status']) => {
        setOrders(orders.map(order =>
            order.id === orderId
                ? { ...order, status: newStatus }
                : order
        ));
    };

    const handleDeleteOrder = (orderId: number) => {
        if (window.confirm('Вы уверены, что хотите удалить этот заказ?')) {
            setOrders(orders.filter(order => order.id !== orderId));
        }
    };

    const handleCreateOrder = () => {
        const newOrder: Order = {
            id: Math.max(...orders.map(o => o.id)) + 1,
            userId: Math.floor(Math.random() * 1000) + 8,
            userName: 'Новый клиент',
            userEmail: 'new@customer.ru',
            items: [],
            total: 0,
            status: 'pending',
            createdAt: new Date().toLocaleString(),
            address: 'Адрес не указан'
        };
        setOrders([newOrder, ...orders]);
    };

    const getStatusName = (status: Order['status']) => {
        switch (status) {
            case 'pending': return 'Ожидает';
            case 'processing': return 'В обработке';
            case 'completed': return 'Завершен';
            case 'cancelled': return 'Отменен';
            default: return status;
        }
    };

    const filteredOrders = orders.filter(order => {
        const matchesSearch =
            order.id.toString().includes(searchTerm) ||
            order.userName.toLowerCase().includes(searchTerm.toLowerCase()) ||
            order.userEmail.toLowerCase().includes(searchTerm.toLowerCase());
        const matchesStatus = statusFilter === 'all' || order.status === statusFilter;
        return matchesSearch && matchesStatus;
    });

    const stats = {
        total: orders.length,
        pending: orders.filter(o => o.status === 'pending').length,
        processing: orders.filter(o => o.status === 'processing').length,
        completed: orders.filter(o => o.status === 'completed').length,
        cancelled: orders.filter(o => o.status === 'cancelled').length,
        totalRevenue: orders.reduce((sum, order) => sum + order.total, 0),
        avgOrder: Math.round(orders.reduce((sum, order) => sum + order.total, 0) / orders.length) || 0,
    };

    const getProductIcon = (productName: string) => {
        if (productName.includes('MacBook') || productName.includes('Ноутбук')) return '💻';
        if (productName.includes('iPhone') || productName.includes('Смартфон')) return '📱';
        if (productName.includes('Apple Watch') || productName.includes('часы')) return '⌚️';
        if (productName.includes('iPad') || productName.includes('Планшет')) return '📱';
        if (productName.includes('AirPods') || productName.includes('Наушники')) return '🎧';
        if (productName.includes('Чехол') || productName.includes('аксессуар')) return '📱';
        return '🛒';
    };

    return (
        <div className="orders-container">
            <div className="orders-header">
                <div>
                    <h2>Управление заказами</h2>
                    <p className="section-subtitle">Всего заказов: {orders.length}</p>
                </div>
                <button className="btn-primary" onClick={handleCreateOrder}>
                    <span>+</span> Создать заказ
                </button>
            </div>

            <div className="stats-cards">
                <div className="stat-card revenue">
                    <div className="stat-icon">💰</div>
                    <div className="stat-value">₽{stats.totalRevenue.toLocaleString()}</div>
                    <div className="stat-label">Общий доход</div>
                </div>
                <div className="stat-card pending">
                    <div className="stat-icon">⏳</div>
                    <div className="stat-value">{stats.pending}</div>
                    <div className="stat-label">Ожидают</div>
                </div>
                <div className="stat-card processing">
                    <div className="stat-icon">🔄</div>
                    <div className="stat-value">{stats.processing}</div>
                    <div className="stat-label">В обработке</div>
                </div>
                <div className="stat-card avg-order">
                    <div className="stat-icon">📊</div>
                    <div className="stat-value">₽{stats.avgOrder.toLocaleString()}</div>
                    <div className="stat-label">Средний заказ</div>
                </div>
            </div>

            <div className="orders-filters">
                <div className="filter-group">
                    <label htmlFor="search">Поиск заказа</label>
                    <input
                        type="text"
                        id="search"
                        placeholder="ID, имя или email клиента..."
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
                    />
                </div>
                <div className="filter-group">
                    <label htmlFor="status-filter">Статус заказа</label>
                    <select
                        id="status-filter"
                        value={statusFilter}
                        onChange={(e) => setStatusFilter(e.target.value)}
                    >
                        <option value="all">Все статусы</option>
                        <option value="pending">Ожидают</option>
                        <option value="processing">В обработке</option>
                        <option value="completed">Завершены</option>
                        <option value="cancelled">Отменены</option>
                    </select>
                </div>
                <div className="filter-group">
                    <label htmlFor="date-filter">Дата</label>
                    <select id="date-filter">
                        <option value="">За все время</option>
                        <option value="today">Сегодня</option>
                        <option value="week">За неделю</option>
                        <option value="month">За месяц</option>
                    </select>
                </div>
            </div>

            <div className="orders-table-container">
                <table className="orders-table">
                    <thead>
                        <tr>
                            <th>ID заказа</th>
                            <th>Клиент</th>
                            <th>Товары</th>
                            <th>Сумма</th>
                            <th>Дата</th>
                            <th>Статус</th>
                            <th>Действия</th>
                        </tr>
                    </thead>
                    <tbody>
                        {filteredOrders.map(order => (
                            <tr key={order.id}>
                                <td className="order-id">#{order.id}</td>
                                <td className="order-customer">
                                    <div className="customer-name">{order.userName}</div>
                                    <div className="customer-email">{order.userEmail}</div>
                                </td>
                                <td className="order-items">
                                    <div className="item-list">
                                        {order.items.map((item, index) => (
                                            <div key={index} className="item">
                                                <span className="item-name">{item.productName}</span>
                                                <span className="item-quantity">×{item.quantity}</span>
                                            </div>
                                        ))}
                                    </div>
                                </td>
                                <td className="order-total">₽{order.total.toLocaleString()}</td>
                                <td>{order.createdAt}</td>
                                <td>
                                    <span className={`order-status status-${order.status}`}>
                                        {getStatusName(order.status)}
                                    </span>
                                </td>
                                <td>
                                    <div className="order-actions">
                                        <button
                                            className="btn-secondary"
                                            onClick={() => handleViewDetails(order)}
                                            style={{ fontSize: '12px', padding: '6px 12px' }}
                                        >
                                            👁️ Просмотр
                                        </button>
                                        <button
                                            className="btn-outline"
                                            onClick={() => handleUpdateStatus(order.id, 'completed')}
                                            style={{ fontSize: '12px', padding: '6px 12px' }}
                                            disabled={order.status === 'completed'}
                                        >
                                            ✅
                                        </button>
                                        <button
                                            className="btn-danger"
                                            onClick={() => handleDeleteOrder(order.id)}
                                            style={{ fontSize: '12px', padding: '6px 12px' }}
                                        >
                                            🗑️
                                        </button>
                                    </div>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>

            {showDetailsModal && selectedOrder && (
                <div className="modal-overlay" onClick={() => setShowDetailsModal(false)}>
                    <div className="modal order-details-modal" onClick={(e) => e.stopPropagation()}>
                        <div className="modal-content">
                            <div className="modal-header">
                                <div className="order-header">
                                    <div className="order-info">
                                        <h3>Заказ #{selectedOrder.id}</h3>
                                        <div className="order-meta">
                                            Создан: {selectedOrder.createdAt} | Клиент: {selectedOrder.userName}
                                        </div>
                                    </div>
                                    <div className="order-actions-top">
                                        <button className="btn-secondary" onClick={() => setShowDetailsModal(false)}>
                                            Закрыть
                                        </button>
                                    </div>
                                </div>
                            </div>

                            <div className="order-sections">
                                <div>
                                    <div className="order-section">
                                        <h4>Товары в заказе</h4>
                                        <div className="items-list">
                                            {selectedOrder.items.map((item, index) => (
                                                <div key={index} className="item-row">
                                                    <div className="item-image">
                                                        {getProductIcon(item.productName)}
                                                    </div>
                                                    <div className="item-details">
                                                        <div className="item-name">{item.productName}</div>
                                                        <div className="item-sku">SKU: {item.sku}</div>
                                                    </div>
                                                    <div className="item-price">
                                                        <div className="item-unit">₽{item.price.toLocaleString()} × {item.quantity}</div>
                                                        <div className="item-total">₽{(item.price * item.quantity).toLocaleString()}</div>
                                                    </div>
                                                </div>
                                            ))}
                                        </div>
                                    </div>

                                    <div className="order-section" style={{ marginTop: '16px' }}>
                                        <h4>Информация о клиенте</h4>
                                        <div className="customer-info">
                                            <div className="info-row">
                                                <div className="info-label">Имя:</div>
                                                <div className="info-value">{selectedOrder.userName}</div>
                                            </div>
                                            <div className="info-row">
                                                <div className="info-label">Email:</div>
                                                <div className="info-value">{selectedOrder.userEmail}</div>
                                            </div>
                                            <div className="info-row">
                                                <div className="info-label">ID клиента:</div>
                                                <div className="info-value">#{selectedOrder.userId}</div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div>
                                    <div className="order-section">
                                        <h4>Сумма заказа</h4>
                                        <div className="order-summary">
                                            <div className="summary-row">
                                                <span>Стоимость товаров:</span>
                                                <span>₽{selectedOrder.total.toLocaleString()}</span>
                                            </div>
                                            <div className="summary-row">
                                                <span>Доставка:</span>
                                                <span>₽0</span>
                                            </div>
                                            <div className="summary-row">
                                                <span>Налог:</span>
                                                <span>Включен</span>
                                            </div>
                                            <div className="summary-row total">
                                                <span>Итого:</span>
                                                <span>₽{selectedOrder.total.toLocaleString()}</span>
                                            </div>
                                        </div>
                                    </div>

                                    <div className="order-section" style={{ marginTop: '16px' }}>
                                        <h4>Статус заказа</h4>
                                        <div className="timeline">
                                            <div className="timeline-item">
                                                <div className={`timeline-dot ${selectedOrder.status === 'pending' || selectedOrder.status === 'processing' || selectedOrder.status === 'completed' ? 'completed' : ''}`}></div>
                                                <div className="timeline-content">
                                                    <div className="timeline-title">Заказ создан</div>
                                                    <div className="timeline-date">{selectedOrder.createdAt}</div>
                                                </div>
                                            </div>
                                            <div className="timeline-item">
                                                <div className={`timeline-dot ${selectedOrder.status === 'processing' || selectedOrder.status === 'completed' ? 'completed' : ''}`}></div>
                                                <div className="timeline-content">
                                                    <div className="timeline-title">В обработке</div>
                                                    <div className="timeline-date">
                                                        {selectedOrder.status === 'processing' || selectedOrder.status === 'completed' ?
                                                            '2024-03-15 15:00' : 'Ожидает обработки'}
                                                    </div>
                                                </div>
                                            </div>
                                            <div className="timeline-item">
                                                <div className={`timeline-dot ${selectedOrder.status === 'completed' ? 'completed' : ''}`}></div>
                                                <div className="timeline-content">
                                                    <div className="timeline-title">Завершен</div>
                                                    <div className="timeline-date">
                                                        {selectedOrder.status === 'completed' ? '2024-03-15 16:30' : 'В процессе'}
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div style={{ marginTop: '16px' }}>
                                            <label style={{ display: 'block', marginBottom: '8px', fontWeight: '600' }}>
                                                Изменить статус:
                                            </label>
                                            <select
                                                value={selectedOrder.status}
                                                onChange={(e) => handleUpdateStatus(selectedOrder.id, e.target.value as Order['status'])}
                                                style={{ width: '100%', padding: '8px', borderRadius: '8px' }}
                                            >
                                                <option value="pending">Ожидает</option>
                                                <option value="processing">В обработке</option>
                                                <option value="completed">Завершен</option>
                                                <option value="cancelled">Отменен</option>
                                            </select>
                                        </div>
                                    </div>

                                    <div className="order-section" style={{ marginTop: '16px' }}>
                                        <h4>Адрес доставки</h4>
                                        <div className="shipping-info">
                                            <div className="info-row">
                                                <div className="info-label">Адрес:</div>
                                                <div className="info-value">{selectedOrder.address}</div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div className="modal-footer">
                                <button
                                    type="button"
                                    className="btn-secondary"
                                    onClick={() => setShowDetailsModal(false)}
                                >
                                    Закрыть
                                </button>
                                <button
                                    type="button"
                                    className="btn-primary"
                                    onClick={() => {
                                        alert('Функция в разработке');
                                    }}
                                >
                                    Распечатать накладную
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};

export default AdminOrders;