import React, { useState } from 'react';
import type { CatalogItem, Category } from '../../components/panel_index';
import './admin_panel_catalog_item.sass';

const AdminCatalogItem: React.FC = () => {
    const [catalogItems, setCatalogItems] = useState<CatalogItem[]>([
        { id: 1, name: 'Ноутбуки', category: 'Электроника', description: 'Портативные компьютеры и ноутбуки', price: 0, sku: 'CAT-001', tags: ['электроника', 'компьютеры'] },
        { id: 2, name: 'Смартфоны', category: 'Электроника', description: 'Мобильные телефоны и аксессуары', price: 0, sku: 'CAT-002', tags: ['электроника', 'телефоны'] },
        { id: 3, name: 'Планшеты', category: 'Электроника', description: 'Планшетные компьютеры', price: 0, sku: 'CAT-003', tags: ['электроника', 'планшеты'] },
        { id: 4, name: 'Наушники', category: 'Аксессуары', description: 'Беспроводные и проводные наушники', price: 0, sku: 'CAT-004', tags: ['аудио', 'аксессуары'] },
        { id: 5, name: 'Умные часы', category: 'Гаджеты', description: 'Смарт-часы и фитнес-трекеры', price: 0, sku: 'CAT-005', tags: ['носимые', 'гаджеты'] },
    ]);

    const [categories, setCategories] = useState<Category[]>([
        { id: 1, name: 'Электроника', productCount: 156 },
        { id: 2, name: 'Компьютеры', productCount: 89 },
        { id: 3, name: 'Смартфоны', productCount: 234 },
        { id: 4, name: 'Периферия', productCount: 412 },
        { id: 5, name: 'Аксессуары', productCount: 567 },
        { id: 6, name: 'Игры', productCount: 198 },
    ]);

    const [showModal, setShowModal] = useState(false);
    const [editingItem, setEditingItem] = useState<CatalogItem | null>(null);
    const [formData, setFormData] = useState({
        name: '',
        category: '',
        description: '',
        price: 0,
        sku: '',
        tags: [] as string[],
    });
    const [newTag, setNewTag] = useState('');

    const handleAddItem = () => {
        setEditingItem(null);
        setFormData({
            name: '',
            category: '',
            description: '',
            price: 0,
            sku: '',
            tags: [],
        });
        setShowModal(true);
    };

    const handleEditItem = (item: CatalogItem) => {
        setEditingItem(item);
        setFormData({
            name: item.name,
            category: item.category,
            description: item.description,
            price: item.price,
            sku: item.sku,
            tags: [...item.tags],
        });
        setShowModal(true);
    };

    const handleDeleteItem = (id: number) => {
        if (window.confirm('Вы уверены, что хотите удалить этот элемент каталога?')) {
            setCatalogItems(catalogItems.filter(item => item.id !== id));
        }
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        if (!formData.name.trim() || !formData.sku.trim()) {
            alert('Пожалуйста, заполните обязательные поля');
            return;
        }

        if (editingItem) {
            setCatalogItems(catalogItems.map(item =>
                item.id === editingItem.id
                    ? { ...item, ...formData }
                    : item
            ));
        } else {
            const newItem: CatalogItem = {
                id: Math.max(...catalogItems.map(i => i.id)) + 1,
                ...formData,
                price: Number(formData.price) || 0,
            };
            setCatalogItems([...catalogItems, newItem]);
        }

        setShowModal(false);
    };

    const handleAddTag = () => {
        if (newTag.trim() && !formData.tags.includes(newTag.trim())) {
            setFormData({
                ...formData,
                tags: [...formData.tags, newTag.trim()]
            });
            setNewTag('');
        }
    };

    const handleRemoveTag = (tag: string) => {
        setFormData({
            ...formData,
            tags: formData.tags.filter(t => t !== tag)
        });
    };

    return (
        <div className="catalog-container">
            <div className="catalog-header">
                <div>
                    <h2>Управление каталогом</h2>
                    <p className="section-subtitle">Категории и элементы каталога</p>
                </div>
                <button className="btn-primary" onClick={handleAddItem}>
                    <span>+</span> Добавить элемент
                </button>
            </div>

            <div className="catalog-filters">
                <div className="filter-card">
                    <h4>Категория</h4>
                    <select>
                        <option value="">Все категории</option>
                        {categories.map(cat => (
                            <option key={cat.id} value={cat.id}>{cat.name}</option>
                        ))}
                    </select>
                </div>
                <div className="filter-card">
                    <h4>Статус</h4>
                    <select>
                        <option value="">Все статусы</option>
                        <option value="active">Активные</option>
                        <option value="inactive">Неактивные</option>
                        <option value="draft">Черновики</option>
                    </select>
                </div>
                <div className="filter-card">
                    <h4>Поиск</h4>
                    <input type="text" placeholder="Поиск по названию или SKU..." />
                </div>
            </div>

            <div className="categories-grid">
                {categories.map(category => (
                    <div key={category.id} className="category-card">
                        <h3>{category.name}</h3>
                        <p className="section-subtitle">Элементов: {category.productCount}</p>
                        <div className="category-info">
                            <span className="product-count">{category.productCount} товаров</span>
                            <div className="category-actions">
                                <button className="btn-outline" style={{ fontSize: '12px', padding: '4px 8px' }}>
                                    Просмотреть
                                </button>
                                <button className="btn-secondary" style={{ fontSize: '12px', padding: '4px 8px' }}>
                                    Редактировать
                                </button>
                            </div>
                        </div>
                    </div>
                ))}
            </div>

            <div className="items-table-container">
                <table className="items-table">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Название</th>
                            <th>Категория</th>
                            <th>SKU</th>
                            <th>Описание</th>
                            <th>Теги</th>
                            <th>Действия</th>
                        </tr>
                    </thead>
                    <tbody>
                        {catalogItems.map(item => (
                            <tr key={item.id}>
                                <td>#{item.id}</td>
                                <td>
                                    <div style={{ fontWeight: '600' }}>{item.name}</div>
                                </td>
                                <td>
                                    <span style={{
                                        padding: '4px 8px',
                                        background: '#f5f5f7',
                                        borderRadius: '6px',
                                        fontSize: '12px'
                                    }}>
                                        {item.category}
                                    </span>
                                </td>
                                <td>
                                    <span className="item-sku">{item.sku}</span>
                                </td>
                                <td>
                                    <div style={{
                                        maxWidth: '200px',
                                        overflow: 'hidden',
                                        textOverflow: 'ellipsis',
                                        whiteSpace: 'nowrap'
                                    }}>
                                        {item.description}
                                    </div>
                                </td>
                                <td>
                                    <div style={{ display: 'flex', gap: '4px', flexWrap: 'wrap' }}>
                                        {/* Исправленная строка 213 - правильный тип для key */}
                                        {item.tags.map((tag: string, index: number) => (
                                            <span key={`${item.id}-${tag}-${index}`} style={{
                                                fontSize: '11px',
                                                padding: '2px 6px',
                                                background: '#f0f0f0',
                                                borderRadius: '10px',
                                                color: '#666'
                                            }}>
                                                {tag}
                                            </span>
                                        ))}
                                    </div>
                                </td>
                                <td>
                                    <div style={{ display: 'flex', gap: '8px' }}>
                                        <button
                                            className="btn-secondary"
                                            onClick={() => handleEditItem(item)}
                                            style={{ fontSize: '12px', padding: '4px 8px' }}
                                        >
                                            ✏️
                                        </button>
                                        <button
                                            className="btn-danger"
                                            onClick={() => handleDeleteItem(item.id)}
                                            style={{ fontSize: '12px', padding: '4px 8px' }}
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

            {showModal && (
                <div className="modal-overlay" onClick={() => setShowModal(false)}>
                    <div className="modal catalog-modal" onClick={(e) => e.stopPropagation()}>
                        <div className="modal-header">
                            <h2>{editingItem ? 'Редактировать элемент' : 'Добавить новый элемент каталога'}</h2>
                            <button className="close-btn" onClick={() => setShowModal(false)}>×</button>
                        </div>
                        <form onSubmit={handleSubmit}>
                            <div className="modal-body">
                                <div className="form-row">
                                    <div className="form-group">
                                        <label htmlFor="name">Название *</label>
                                        <input
                                            type="text"
                                            id="name"
                                            value={formData.name}
                                            onChange={(e) => setFormData({ ...formData, name: e.target.value })}
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
                                        <label htmlFor="category">Категория *</label>
                                        <select
                                            id="category"
                                            value={formData.category}
                                            onChange={(e) => setFormData({ ...formData, category: e.target.value })}
                                            required
                                        >
                                            <option value="">Выберите категорию</option>
                                            {categories.map(cat => (
                                                <option key={cat.id} value={cat.name}>{cat.name}</option>
                                            ))}
                                        </select>
                                    </div>
                                    <div className="form-group">
                                        <label htmlFor="price">Цена</label>
                                        <input
                                            type="number"
                                            id="price"
                                            value={formData.price}
                                            onChange={(e) => setFormData({ ...formData, price: Number(e.target.value) })}
                                            min="0"
                                            step="0.01"
                                        />
                                    </div>
                                </div>
                                <div className="form-group">
                                    <label htmlFor="description">Описание</label>
                                    <textarea
                                        id="description"
                                        value={formData.description}
                                        onChange={(e) => setFormData({ ...formData, description: e.target.value })}
                                        rows={3}
                                    />
                                </div>
                                <div className="form-group">
                                    <label>Теги</label>
                                    <div className="tags-container">
                                        {formData.tags.map((tag, index) => (
                                            <div key={`${editingItem?.id || 'new'}-${tag}-${index}`} className="tag">
                                                {tag}
                                                <button type="button" onClick={() => handleRemoveTag(tag)}>×</button>
                                            </div>
                                        ))}
                                    </div>
                                    <div className="add-tag">
                                        <input
                                            type="text"
                                            value={newTag}
                                            onChange={(e) => setNewTag(e.target.value)}
                                            placeholder="Добавить тег"
                                            onKeyPress={(e) => e.key === 'Enter' && (e.preventDefault(), handleAddTag())}
                                        />
                                        <button type="button" className="btn-secondary" onClick={handleAddTag}>
                                            Добавить
                                        </button>
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
                                    {editingItem ? 'Сохранить изменения' : 'Создать элемент'}
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            )}
        </div>
    );
};

export default AdminCatalogItem;