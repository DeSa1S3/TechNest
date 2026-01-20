import React, { useState, useEffect } from 'react';
import { catalogService } from '../../service/apiServices';
import './admin_panel_catalog_item.sass';

interface Category {
    id: number;
    name: string;
    description?: string;
    productCount: number;
}

interface CatalogItem {
    id: number;
    name: string;
    category: string;
    description: string;
    price: number;
    sku: string;
    tags: string[];
}

const AdminCatalogItem: React.FC = () => {
    const [catalogItems, setCatalogItems] = useState<CatalogItem[]>([]);
    const [categories, setCategories] = useState<Category[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
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

    useEffect(() => {
        loadCategories();
    }, []);

    const loadCategories = async () => {
        try {
            setLoading(true);
            setError(null);

            const data = await catalogService.getAllCategories();

            const formattedCategories: Category[] = data.map((cat: any) => ({
                id: cat.id,
                name: cat.name,
                description: cat.description,
                productCount: cat.subCategories?.length || 0
            }));

            const formattedCatalogItems: CatalogItem[] = data.flatMap((cat: any) =>
                cat.subCategories?.map((subCat: any) => ({
                    id: subCat.id,
                    name: subCat.name,
                    category: cat.name,
                    description: subCat.description || '',
                    price: 0,
                    sku: `CAT-${subCat.id}`,
                    tags: [cat.name.toLowerCase()]
                })) || []
            );

            setCategories(formattedCategories);
            setCatalogItems(formattedCatalogItems);

        } catch (err: any) {
            setError(`Ошибка загрузки: ${err.message}`);
            console.error('Ошибка загрузки категорий:', err);
        } finally {
            setLoading(false);
        }
    };

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

    const handleDeleteItem = async (id: number) => {
        if (window.confirm('Вы уверены, что хотите удалить этот элемент каталога?')) {
            try {
                await catalogService.deleteCategory(id);
                await loadCategories();
                alert('Элемент успешно удален');
            } catch (err: any) {
                alert(`Ошибка удаления: ${err.message}`);
            }
        }
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        if (!formData.name.trim()) {
            alert('Пожалуйста, заполните название');
            return;
        }

        try {
            const categoryData = {
                name: formData.name,
                description: formData.description,
                parentCategoryId: categories.find(c => c.name === formData.category)?.id || null
            };

            if (editingItem) {
                await catalogService.updateCategory(editingItem.id, categoryData);
            } else {
                await catalogService.createCategory(categoryData);
            }

            await loadCategories();
            setShowModal(false);
            alert(editingItem ? 'Элемент успешно обновлен' : 'Элемент успешно создан');

        } catch (err: any) {
            alert(`Ошибка сохранения: ${err.message}`);
        }
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

    if (loading) {
        return (
            <div className="catalog-container">
                <div className="loading">Загрузка данных...</div>
            </div>
        );
    }

    if (error) {
        return (
            <div className="catalog-container">
                <div className="error-message">
                    Ошибка: {error}
                    <button onClick={loadCategories} className="btn-primary">
                        Попробовать снова
                    </button>
                </div>
            </div>
        );
    }

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