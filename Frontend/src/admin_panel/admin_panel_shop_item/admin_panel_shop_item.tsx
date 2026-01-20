import React, { useState, useEffect } from 'react';
import type { ShopItem } from '../../components/panel_index';
import { shopItemsService } from '../../service/apiServices';
import './admin_panel_shop_item.sass';

interface ColorOption {
    name: string;
    value: string;
}

const AdminShopItem: React.FC = () => {
    const [activeTab, setActiveTab] = useState<'all' | 'in-stock' | 'out-of-stock' | 'draft'>('all');
    const [shopItems, setShopItems] = useState<ShopItem[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const [showModal, setShowModal] = useState(false);
    const [editingItem, setEditingItem] = useState<ShopItem | null>(null);
    const [formData, setFormData] = useState({
        title: '',
        sku: '',
        description: '',
        price: 0,
        category: '',
        specifications: [] as string[],
        availability: true,
        color: 'Черный',
        rating: 0,
        reviews: 0,
    });
    const [newSpec, setNewSpec] = useState('');

    const colors: ColorOption[] = [
        { name: 'Черный', value: '#1d1d1f' },
        { name: 'Белый', value: '#f5f5f7' },
        { name: 'Серебристый', value: '#d2d2d7' },
        { name: 'Серый космос', value: '#424245' },
        { name: 'Титан', value: '#86868b' },
        { name: 'Синий', value: '#0071e3' },
        { name: 'Зеленый', value: '#34c759' },
        { name: 'Красный', value: '#ff3b30' },
    ];

    const getCategoryIcon = (category: string): string => {
        switch (category) {
            case 'Ноутбуки': return '💻';
            case 'Смартфоны': return '📱';
            case 'Умные часы': return '⌚️';
            case 'Планшеты': return '📱';
            default: return '🛒';
        }
    };

    useEffect(() => {
        loadShopItems();
    }, []);

    const loadShopItems = async () => {
        try {
            setLoading(true);
            setError(null);

            // const data = await shopItemsService.getAllShopItems(1, 20);

            const mockData: ShopItem[] = [
                {
                    id: 1,
                    title: 'Ноутбук Apple MacBook Air',
                    sku: '88-999-P',
                    description: '15.6" Ноутбук Apple MacBook Air черный. Английская/русская раскладка, 2560x1684, IPS, Apple M2 8-core, ядра: 4 + 4, RAM 8 ГБ, SSD 256 ГБ, macOS',
                    price: 80799,
                    category: 'Ноутбуки',
                    specifications: ['15.6" дисплей', 'Apple M2 8-core', '8 ГБ RAM', '256 ГБ SSD', 'macOS'],
                    availability: true,
                    color: 'Черный',
                    rating: 4.25,
                    reviews: 17
                },
                {
                    id: 2,
                    title: 'iPhone 15 Pro Max 256GB',
                    sku: '77-888-P',
                    description: 'Смартфон Apple iPhone 15 Pro Max 256GB с дисплеем 6.7", процессором A17 Pro, камерой 48 Мп',
                    price: 125990,
                    category: 'Смартфоны',
                    specifications: ['6.7" Super Retina XDR', 'A17 Pro', '48 Мп камера', '256 ГБ', 'iOS 17'],
                    availability: true,
                    color: 'Титан',
                    rating: 4.8,
                    reviews: 45
                },
                {
                    id: 3,
                    title: 'Apple Watch Series 9',
                    sku: '66-777-P',
                    description: 'Умные часы Apple Watch Series 9 45mm с функцией Always-On дисплея и отслеживанием здоровья',
                    price: 45990,
                    category: 'Умные часы',
                    specifications: ['45mm', 'Always-On', 'GPS + Cellular', 'Без рамки', 'watchOS 10'],
                    availability: false,
                    color: 'Серебристый',
                    rating: 4.6,
                    reviews: 23
                },
                {
                    id: 4,
                    title: 'iPad Pro 12.9" M2',
                    sku: '55-666-P',
                    description: 'Планшет Apple iPad Pro 12.9" с чипом M2, дисплеем Liquid Retina XDR и поддержкой Apple Pencil',
                    price: 139990,
                    category: 'Планшеты',
                    specifications: ['12.9" Liquid Retina', 'M2 чип', '1 ТБ SSD', '5G', 'iPadOS 16'],
                    availability: true,
                    color: 'Серый космос',
                    rating: 4.7,
                    reviews: 31
                },
            ];

            setShopItems(mockData);

        } catch (err: any) {
            setError(`Ошибка загрузки: ${err.message}`);
            console.error('Ошибка загрузки товаров:', err);
        } finally {
            setLoading(false);
        }
    };

    const handleAddItem = () => {
        setEditingItem(null);
        setFormData({
            title: '',
            sku: '',
            description: '',
            price: 0,
            category: '',
            specifications: [],
            availability: true,
            color: 'Черный',
            rating: 0,
            reviews: 0,
        });
        setShowModal(true);
    };

    const handleEditItem = (item: ShopItem) => {
        setEditingItem(item);
        setFormData({
            title: item.title,
            sku: item.sku,
            description: item.description,
            price: item.price,
            category: item.category,
            specifications: [...item.specifications],
            availability: item.availability,
            color: item.color,
            rating: item.rating,
            reviews: item.reviews,
        });
        setShowModal(true);
    };

    const handleDeleteItem = async (id: number) => {
        if (window.confirm('Вы уверены, что хотите удалить этот товар?')) {
            try {
                await shopItemsService.deleteShopItem(id);
                setShopItems(prevItems => prevItems.filter(item => item.id !== id));
                alert('Товар успешно удален');
            } catch (err: any) {
                alert(`Ошибка удаления: ${err.message}`);
            }
        }
    };

    const handleAddSpec = () => {
        if (newSpec.trim() && !formData.specifications.includes(newSpec.trim())) {
            setFormData({
                ...formData,
                specifications: [...formData.specifications, newSpec.trim()]
            });
            setNewSpec('');
        }
    };

    const handleRemoveSpec = (spec: string) => {
        setFormData({
            ...formData,
            specifications: formData.specifications.filter(s => s !== spec)
        });
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        if (!formData.title.trim() || !formData.sku.trim()) {
            alert('Пожалуйста, заполните обязательные поля');
            return;
        }

        try {
            const shopItemData = {
                name: formData.title,
                description: formData.description,
                price: formData.price,
                category: formData.category,
                specifications: formData.specifications,
                quantity: formData.availability ? 10 : 0,
                availability: formData.availability
            };

            if (editingItem) {
                await shopItemsService.updateShopItem(editingItem.id, shopItemData);

                setShopItems(prevItems =>
                    prevItems.map(item =>
                        item.id === editingItem.id
                            ? {
                                ...item,
                                title: formData.title,
                                sku: formData.sku,
                                description: formData.description,
                                price: formData.price,
                                category: formData.category,
                                specifications: formData.specifications,
                                availability: formData.availability,
                                color: formData.color,
                                rating: formData.rating,
                                reviews: formData.reviews
                            }
                            : item
                    )
                );
            } else {
                await shopItemsService.createShopItem(shopItemData);

                const newShopItem: ShopItem = {
                    id: Math.max(...shopItems.map(i => i.id)) + 1,
                    title: formData.title,
                    sku: formData.sku,
                    description: formData.description,
                    price: formData.price,
                    category: formData.category,
                    specifications: formData.specifications,
                    availability: formData.availability,
                    color: formData.color,
                    rating: formData.rating,
                    reviews: formData.reviews
                };
                setShopItems(prevItems => [...prevItems, newShopItem]);
            }

            setShowModal(false);
            alert(editingItem ? 'Товар успешно обновлен' : 'Товар успешно создан');

        } catch (err: any) {
            alert(`Ошибка сохранения: ${err.message}`);
        }
    };

    const filteredItems = shopItems.filter(item => {
        if (activeTab === 'all') return true;
        if (activeTab === 'in-stock') return item.availability;
        if (activeTab === 'out-of-stock') return !item.availability;
        return true;
    });

    if (loading) {
        return (
            <div className="shop-container">
                <div className="loading">Загрузка товаров...</div>
            </div>
        );
    }

    if (error) {
        return (
            <div className="shop-container">
                <div className="error-message">
                    Ошибка: {error}
                    <button onClick={loadShopItems} className="btn-primary">
                        Попробовать снова
                    </button>
                </div>
            </div>
        );
    }

    return (
        <div className="shop-container">
            <div className="shop-header">
                <div>
                    <h2>Управление продукцией</h2>
                    <p className="section-subtitle">Создание и редактирование товаров</p>
                </div>
                <button className="btn-primary" onClick={handleAddItem}>
                    <span>+</span> Добавить товар
                </button>
            </div>

            <div className="tabs">
                <button
                    className={`tab ${activeTab === 'all' ? 'active' : ''}`}
                    onClick={() => setActiveTab('all')}
                >
                    Все товары ({shopItems.length})
                </button>
                <button
                    className={`tab ${activeTab === 'in-stock' ? 'active' : ''}`}
                    onClick={() => setActiveTab('in-stock')}
                >
                    В наличии ({shopItems.filter(i => i.availability).length})
                </button>
                <button
                    className={`tab ${activeTab === 'out-of-stock' ? 'active' : ''}`}
                    onClick={() => setActiveTab('out-of-stock')}
                >
                    Нет в наличии ({shopItems.filter(i => !i.availability).length})
                </button>
            </div>

            <div className="products-grid">
                {filteredItems.map((item: ShopItem) => (
                    <div key={item.id} className="product-card-large">
                        <div className="product-image">
                            {getCategoryIcon(item.category)}
                            <div className={`product-status ${item.availability ? 'in-stock' : 'out-of-stock'}`}>
                                {item.availability ? 'В наличии' : 'Нет в наличии'}
                            </div>
                        </div>
                        <div className="product-content">
                            <h3>{item.title}</h3>
                            <p className="product-sku">SKU: {item.sku}</p>
                            <p className="product-description">{item.description}</p>
                            <div className="product-price">₽{item.price.toLocaleString()}</div>

                            <div className="product-specs">
                                <div className="spec-item">
                                    <span className="spec-label">Категория:</span>
                                    <span className="spec-value">{item.category}</span>
                                </div>
                                <div className="spec-item">
                                    <span className="spec-label">Цвет:</span>
                                    <span className="spec-value">{item.color}</span>
                                </div>
                                <div className="spec-item">
                                    <span className="spec-label">Рейтинг:</span>
                                    <span className="spec-value">{item.rating} ★ ({item.reviews} отзывов)</span>
                                </div>
                            </div>

                            <div className="product-meta">
                                <div className="product-rating">
                                    <span className="stars">★★★★★</span>
                                    <span>{item.rating}</span>
                                </div>
                                <div className="product-reviews">
                                    {item.reviews} отзывов
                                </div>
                            </div>

                            <div className="product-actions">
                                <button
                                    className="btn-outline"
                                    onClick={() => handleEditItem(item)}
                                >
                                    ✏️ Редактировать
                                </button>
                                <button
                                    className="btn-danger"
                                    onClick={() => handleDeleteItem(item.id)}
                                >
                                    🗑️ Удалить
                                </button>
                            </div>
                        </div>
                    </div>
                ))}
            </div>

            {showModal && (
                <div className="modal-overlay" onClick={() => setShowModal(false)}>
                    <div className="modal shop-modal" onClick={(e) => e.stopPropagation()}>
                        <div className="modal-header">
                            <h2>{editingItem ? 'Редактировать товар' : 'Добавить новый товар'}</h2>
                            <button className="close-btn" onClick={() => setShowModal(false)}>×</button>
                        </div>
                        <form onSubmit={handleSubmit}>
                            <div className="modal-body">
                                <div className="form-section">
                                    <h3>Основная информация</h3>
                                    <div className="form-row">
                                        <div className="form-group">
                                            <label htmlFor="title">Название товара *</label>
                                            <input
                                                type="text"
                                                id="title"
                                                value={formData.title}
                                                onChange={(e) => setFormData({ ...formData, title: e.target.value })}
                                                required
                                            />
                                        </div>
                                        <div className="form-group">
                                            <label htmlFor="sku">SKU *</label>
                                            <input
                                                type="text"
                                                id="sku"
                                                value={formData.sku}
                                                onChange={(e) => setFormData({ ...formData, sku: e.target.value })}
                                                required
                                            />
                                        </div>
                                    </div>
                                    <div className="form-row">
                                        <div className="form-group">
                                            <label htmlFor="price">Цена (₽) *</label>
                                            <input
                                                type="number"
                                                id="price"
                                                value={formData.price}
                                                onChange={(e) => setFormData({ ...formData, price: Number(e.target.value) })}
                                                min="0"
                                                step="100"
                                                required
                                            />
                                        </div>
                                        <div className="form-group">
                                            <label htmlFor="category">Категория *</label>
                                            <select
                                                id="category"
                                                value={formData.category}
                                                onChange={(e) => setFormData({ ...formData, category: e.target.value })}
                                                required
                                            >
                                                <option value="">Выберите категорию</option>
                                                <option value="Ноутбуки">Ноутбуки</option>
                                                <option value="Смартфоны">Смартфоны</option>
                                                <option value="Планшеты">Планшеты</option>
                                                <option value="Умные часы">Умные часы</option>
                                                <option value="Наушники">Наушники</option>
                                                <option value="Компьютеры">Компьютеры</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div className="form-group">
                                        <label htmlFor="description">Описание</label>
                                        <textarea
                                            id="description"
                                            value={formData.description}
                                            onChange={(e) => setFormData({ ...formData, description: e.target.value })}
                                            rows={4}
                                            placeholder="Подробное описание товара..."
                                        />
                                    </div>
                                </div>

                                <div className="form-section">
                                    <h3>Характеристики</h3>
                                    <div className="specs-container">
                                        <div className="spec-input">
                                            <input
                                                type="text"
                                                value={newSpec}
                                                onChange={(e) => setNewSpec(e.target.value)}
                                                placeholder="Добавить характеристику"
                                                onKeyPress={(e) => e.key === 'Enter' && (e.preventDefault(), handleAddSpec())}
                                            />
                                            <button type="button" className="btn-secondary" onClick={handleAddSpec}>
                                                Добавить
                                            </button>
                                        </div>
                                        <div className="specs-list">
                                            {formData.specifications.map((spec, index) => (
                                                <div key={index} className="spec-tag">
                                                    {spec}
                                                    <button type="button" onClick={() => handleRemoveSpec(spec)}>×</button>
                                                </div>
                                            ))}
                                        </div>
                                    </div>
                                </div>

                                <div className="form-section">
                                    <h3>Детали</h3>
                                    <div className="form-row">
                                        <div className="form-group">
                                            <label>Цвет</label>
                                            <div className="color-options">
                                                {colors.map((color: ColorOption, index: number) => (
                                                    <div
                                                        key={index}
                                                        className={`color-option ${formData.color === color.name ? 'selected' : ''}`}
                                                        style={{ backgroundColor: color.value }}
                                                        onClick={() => setFormData({ ...formData, color: color.name })}
                                                        title={color.name}
                                                    >
                                                        <span className="color-name">{formData.color === color.name ? color.name : ''}</span>
                                                    </div>
                                                ))}
                                            </div>
                                        </div>
                                        <div className="form-group">
                                            <label htmlFor="availability">Наличие</label>
                                            <select
                                                id="availability"
                                                value={formData.availability.toString()}
                                                onChange={(e) => setFormData({ ...formData, availability: e.target.value === 'true' })}
                                            >
                                                <option value="true">В наличии</option>
                                                <option value="false">Нет в наличии</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div className="form-row">
                                        <div className="form-group">
                                            <label htmlFor="rating">Рейтинг</label>
                                            <input
                                                type="number"
                                                id="rating"
                                                value={formData.rating}
                                                onChange={(e) => setFormData({ ...formData, rating: Number(e.target.value) })}
                                                min="0"
                                                max="5"
                                                step="0.1"
                                            />
                                        </div>
                                        <div className="form-group">
                                            <label htmlFor="reviews">Количество отзывов</label>
                                            <input
                                                type="number"
                                                id="reviews"
                                                value={formData.reviews}
                                                onChange={(e) => setFormData({ ...formData, reviews: Number(e.target.value) })}
                                                min="0"
                                            />
                                        </div>
                                    </div>
                                </div>

                                <div className="form-section">
                                    <h3>Изображение товара</h3>
                                    <div className="image-upload">
                                        <div className="upload-icon">📷</div>
                                        <div className="upload-text">Перетащите изображение или нажмите для загрузки</div>
                                        <div className="upload-hint">PNG, JPG до 5MB</div>
                                    </div>
                                </div>
                            </div>
                            <div className="modal-footer">
                                <button
                                    type="button"
                                    className="btn-secondary"
                                    onClick={() => setShowModal(false)}
                                >
                                    Отмена
                                </button>
                                <button type="submit" className="btn-primary">
                                    {editingItem ? 'Сохранить изменения' : 'Создать товар'}
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            )}
        </div>
    );
};

export default AdminShopItem;