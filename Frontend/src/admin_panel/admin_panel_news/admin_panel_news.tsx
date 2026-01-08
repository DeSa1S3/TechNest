import React, { useState } from 'react';
import type { News } from '../../components/panel_index';
import './admin_panel_news.sass';

const AdminNews: React.FC = () => {
    const [news, setNews] = useState<News[]>([
        {
            id: 1,
            title: 'Обновление каталога техники',
            content: 'Мы рады сообщить о значительном обновлении нашего каталога. Добавлены новые модели ноутбуков, смартфонов и планшетов от ведущих производителей.',
            author: 'Редактор',
            createdAt: '2024-03-15',
            isPublished: true
        },
        {
            id: 2,
            title: 'Скидки на Apple технику до 30%',
            content: 'Только этой весной специальные скидки на всю технику Apple. Успейте приобрести по выгодным ценам!',
            author: 'Администратор',
            createdAt: '2024-03-10',
            isPublished: true
        },
        {
            id: 3,
            title: 'Новая линейка смарт-часов',
            content: 'Представляем новую линейку умных часов с расширенными функциями отслеживания здоровья и улучшенной автономностью.',
            author: 'Редактор',
            createdAt: '2024-03-05',
            isPublished: false
        },
        {
            id: 4,
            title: 'Итоги года: самые популярные товары',
            content: 'Подводим итоги года и рассказываем о самых популярных товарах в нашем магазине. Узнайте, что выбирали наши покупатели.',
            author: 'Администратор',
            createdAt: '2024-02-28',
            isPublished: true
        },
    ]);

    const [showModal, setShowModal] = useState(false);
    const [editingNews, setEditingNews] = useState<News | null>(null);
    const [formData, setFormData] = useState({
        title: '',
        content: '',
        author: 'Администратор',
        isPublished: true,
    });

    const handleAddNews = () => {
        setEditingNews(null);
        setFormData({
            title: '',
            content: '',
            author: 'Администратор',
            isPublished: true,
        });
        setShowModal(true);
    };

    const handleEditNews = (newsItem: News) => {
        setEditingNews(newsItem);
        setFormData({
            title: newsItem.title,
            content: newsItem.content,
            author: newsItem.author,
            isPublished: newsItem.isPublished,
        });
        setShowModal(true);
    };

    const handleDeleteNews = (id: number) => {
        if (window.confirm('Вы уверены, что хотите удалить эту новость?')) {
            setNews(news.filter(item => item.id !== id));
        }
    };

    const handleTogglePublish = (id: number) => {
        setNews(news.map(item =>
            item.id === id
                ? { ...item, isPublished: !item.isPublished }
                : item
        ));
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        if (!formData.title.trim() || !formData.content.trim()) {
            alert('Пожалуйста, заполните обязательные поля');
            return;
        }

        if (editingNews) {
            setNews(news.map(item =>
                item.id === editingNews.id
                    ? {
                        ...item,
                        ...formData,
                        createdAt: item.createdAt
                    }
                    : item
            ));
        } else {
            const newNews: News = {
                id: Math.max(...news.map(n => n.id)) + 1,
                ...formData,
                createdAt: new Date().toISOString().split('T')[0],
            };
            setNews([...news, newNews]);
        }

        setShowModal(false);
    };

    const getNewsIcon = (title: string) => {
        const keywords = {
            'Apple': '🍎',
            'скидк': '🎯',
            'новый': '🆕',
            'обнов': '🔄',
            'итог': '📊',
            'популярн': '🔥',
            'техник': '💻',
            'смарт': '📱',
        };

        for (const [keyword, icon] of Object.entries(keywords)) {
            if (title.toLowerCase().includes(keyword.toLowerCase())) {
                return icon;
            }
        }
        return '📰';
    };

    const publishedNews = news.filter(item => item.isPublished);
    const draftNews = news.filter(item => !item.isPublished);

    return (
        <div className="news-container">
            <div className="news-header">
                <div>
                    <h2>Управление новостями</h2>
                    <p className="section-subtitle">Создание и публикация новостей</p>
                </div>
                <button className="btn-primary" onClick={handleAddNews}>
                    <span>+</span> Добавить новость
                </button>
            </div>

            <div className="news-stats">
                <div className="stat-card">
                    <div className="stat-icon">📰</div>
                    <div className="stat-value">{news.length}</div>
                    <div className="stat-label">Всего новостей</div>
                </div>
                <div className="stat-card">
                    <div className="stat-icon">✅</div>
                    <div className="stat-value">{publishedNews.length}</div>
                    <div className="stat-label">Опубликовано</div>
                </div>
                <div className="stat-card">
                    <div className="stat-icon">✏️</div>
                    <div className="stat-value">{draftNews.length}</div>
                    <div className="stat-label">Черновики</div>
                </div>
                <div className="stat-card">
                    <div className="stat-icon">👥</div>
                    <div className="stat-value">{[...new Set(news.map(n => n.author))].length}</div>
                    <div className="stat-label">Авторов</div>
                </div>
            </div>

            <div className="news-filters">
                <div className="filter-card">
                    <h4>Статус</h4>
                    <select defaultValue="">
                        <option value="">Все новости</option>
                        <option value="published">Опубликованные</option>
                        <option value="draft">Черновики</option>
                    </select>
                </div>
                <div className="filter-card">
                    <h4>Автор</h4>
                    <select defaultValue="">
                        <option value="">Все авторы</option>
                        {[...new Set(news.map(n => n.author))].map(author => (
                            <option key={author} value={author}>{author}</option>
                        ))}
                    </select>
                </div>
                <div className="filter-card">
                    <h4>Поиск</h4>
                    <input type="text" placeholder="Поиск по заголовку..." />
                </div>
            </div>

            <div className="news-grid">
                {news.map(item => (
                    <div key={item.id} className="news-card">
                        <div className="news-image">
                            {getNewsIcon(item.title)}
                            <div className={`news-status ${item.isPublished ? 'published' : 'draft'}`}>
                                {item.isPublished ? 'Опубликовано' : 'Черновик'}
                            </div>
                        </div>
                        <div className="news-content">
                            <h3>{item.title}</h3>
                            <p className="news-excerpt">{item.content}</p>

                            <div className="news-meta">
                                <div className="news-author">
                                    <div className="author-avatar">
                                        {item.author.charAt(0)}
                                    </div>
                                    <span>{item.author}</span>
                                </div>
                                <div className="news-date">
                                    {item.createdAt}
                                </div>
                            </div>

                            <div className="news-actions">
                                <button
                                    className="btn-outline"
                                    onClick={() => handleEditNews(item)}
                                >
                                    ✏️ Редактировать
                                </button>
                                <button
                                    className={`${item.isPublished ? 'btn-secondary' : 'btn-success'}`}
                                    onClick={() => handleTogglePublish(item.id)}
                                >
                                    {item.isPublished ? '❌ Снять' : '✅ Опубликовать'}
                                </button>
                                <button
                                    className="btn-danger"
                                    onClick={() => handleDeleteNews(item.id)}
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
                    <div className="modal news-modal" onClick={(e) => e.stopPropagation()}>
                        <div className="modal-header">
                            <h2>{editingNews ? 'Редактировать новость' : 'Добавить новую новость'}</h2>
                            <button className="close-btn" onClick={() => setShowModal(false)}>×</button>
                        </div>
                        <form onSubmit={handleSubmit}>
                            <div className="modal-body">
                                <div className="form-group">
                                    <label htmlFor="title">Заголовок новости *</label>
                                    <input
                                        type="text"
                                        id="title"
                                        value={formData.title}
                                        onChange={(e) => setFormData({ ...formData, title: e.target.value })}
                                        placeholder="Введите заголовок новости"
                                        required
                                    />
                                </div>
                                <div className="form-group">
                                    <label htmlFor="author">Автор *</label>
                                    <select
                                        id="author"
                                        value={formData.author}
                                        onChange={(e) => setFormData({ ...formData, author: e.target.value })}
                                        required
                                    >
                                        <option value="Администратор">Администратор</option>
                                        <option value="Редактор">Редактор</option>
                                        <option value="Менеджер">Менеджер</option>
                                    </select>
                                </div>
                                <div className="form-group">
                                    <label htmlFor="content">Содержание новости *</label>
                                    <div className="editor-container">
                                        <div className="editor-toolbar">
                                            <button type="button">B</button>
                                            <button type="button">I</button>
                                            <button type="button">U</button>
                                            <button type="button">H1</button>
                                            <button type="button">H2</button>
                                            <button type="button">📷</button>
                                            <button type="button">🔗</button>
                                        </div>
                                        <div
                                            className="editor-content"
                                            contentEditable
                                            onInput={(e) => setFormData({ ...formData, content: e.currentTarget.textContent || '' })}
                                            dangerouslySetInnerHTML={{ __html: formData.content }}
                                        />
                                    </div>
                                </div>
                                <div className="form-group">
                                    <label htmlFor="isPublished">Статус</label>
                                    <select
                                        id="isPublished"
                                        value={formData.isPublished.toString()}
                                        onChange={(e) => setFormData({ ...formData, isPublished: e.target.value === 'true' })}
                                    >
                                        <option value="true">Опубликовано</option>
                                        <option value="false">Черновик</option>
                                    </select>
                                </div>
                                <div className="form-group">
                                    <label>Изображение новости</label>
                                    <div className="image-upload" style={{ marginTop: '8px' }}>
                                        <div className="upload-icon">📷</div>
                                        <div className="upload-text">Загрузить изображение</div>
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
                                    {editingNews ? 'Сохранить изменения' : 'Создать новость'}
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            )}
        </div>
    );
};

export default AdminNews;