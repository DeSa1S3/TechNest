import { useState } from 'react'
import './household-appliances.sass'

function householdAppliances_pages() {
    const [count, setCount] = useState(0)

    const products = [
        {
            id: 1,
            name: "Встраиваемый холодильник Gorenje",
            description: "[275л, нижняя морозильная камера]",
            price: "88 999 ₽",
            monthlyPayment: "от 3 778 ₽/мес.",
            rating: 4.5,
            reviews: "105",
            availability: "В наличии в 3 магазинах",
            code: "5475586",
            isHit: false
        },
        {
            id: 2,
            name: "Встраиваемый холодильник DEXP Fresh BIB420AMA",
            description: "[241л, нижняя морозильная камера]",
            price: "30 999 ₽",
            monthlyPayment: "от 1 295 ₽/мес.",
            rating: 4.62,
            reviews: "492",
            reliability: "Отличная надежность",
            code: "5475587",
            isHit: false
        },
        {
            id: 3,
            name: "Встраиваемый холодильник DEXP BIB4-30AMA",
            description: "[300л, нижняя морозильная камера]",
            price: "35 999 ₽",
            monthlyPayment: "от 1 698 ₽/мес.",
            rating: 4.36,
            reviews: "74",
            reliability: "Отличная надежность",
            code: "1247270",
            isHit: true,
            availability: "В наличии в 10 магазинах",
            delivery: "Доставим на дом сегодня",
            pickup: "Пункты выдачи доступны"
        },
        {
            id: 4,
            name: "Встраиваемый холодильник DEXP BIB4-28AHE",
            description: "[275л, нижняя морозильная камера]",
            price: "50 999 ₽",
            monthlyPayment: "от 2 128 ₽/мес.",
            rating: 4.15,
            reviews: "16",
            reliability: "Отличная надежность",
            delivery: "Доставим на дом сегодня",
            code: "5475589",
            isHit: false
        }
    ]

    const additionalFilters = [
        "Мощность (Вт)",
        "Высота",
        "Размораживание морозильной камеры",
        "Наличие морозильной камеры",
        "Кол-во дверей",
        "Инверторный компрессор",
        "Общий полезной объем",
        "Ширина встраивания (см)",
        "Управление со смартфона",
        "Приложение для управления"
    ]

    const hotTopics = [
        "Как выбрать встраиваемый холодильник",
        "Как выбратьхолодильник",
        "Для чего нужны инверторные холодильники",
        "Антибактериальные покрытие - это как работает?"
    ]
    return (
        <>
            <div className="beauty-health-container">
                <header className="page-header">
                    <h1 className="page-title">Бытовая техника</h1>
                </header>

                <div className="content-wrapper">
                    <aside className="filters-sidebar">
                        <div className="filters-section">
                            <h3 className="filters-title">Поиск по фильтрам</h3>
                            <div className="filter-options">
                                <label className="filter-option">
                                    <input type="checkbox" />
                                    <span className="checkmark"></span>
                                    No frost
                                </label>
                                <label className="filter-option">
                                    <input type="checkbox" />
                                    <span className="checkmark"></span>
                                    Двухкамерные
                                </label>
                                <label className="filter-option">
                                    <input type="checkbox" />
                                    <span className="checkmark"></span>
                                    Без морозильной камеры
                                </label>
                                <label className="filter-option">
                                    <input type="checkbox" />
                                    <span className="checkmark"></span>
                                    Двухдверные
                                </label>
                                <label className="filter-option">
                                    <input type="checkbox" />
                                    <span className="checkmark"></span>
                                    Инверторные
                                </label>
                            </div>
                            <button className="show-more-btn">Показать ещё</button>
                        </div>

                        <div className="filters-section">
                            <h3 className="filters-title">Smart-консультант</h3>
                            <p className="consultant-text">Данные ссылки помогут сделать правильный выбор:</p>

                            <div className="consultant-advice">
                                <h4 className="advice-title">Советы</h4>
                                <div className="advice-links">
                                    <a href="#" className="advice-link">Гид на клубе DNS</a>
                                    <a href="#" className="advice-link">Видео гид</a>
                                    <a href="#" className="advice-link">Инфографика</a>
                                </div>
                            </div>
                        </div>

                        <div className="filters-section">
                            <h3 className="filters-title">Наличие в магазинах</h3>
                            <div className="stores-info">
                                <p>В любом из 14 магазинов</p>
                            </div>
                        </div>

                        <div className="filters-section">
                            <h3 className="filters-title">Рейтинг и надёжность</h3>
                            <div className="rating-options">
                                <label className="rating-option">
                                    <input type="checkbox" />
                                    <span className="checkmark"></span>
                                    Рейтинг 4 и выше (355)
                                </label>
                                <label className="rating-option">
                                    <input type="checkbox" />
                                    <span className="checkmark"></span>
                                    Надёжные модели (388) минимум обращений в сервис
                                </label>
                                <label className="rating-option">
                                    <input type="checkbox" />
                                    <span className="checkmark"></span>
                                    Есть обзор (113)
                                </label>
                            </div>
                        </div>

                        <div className="filters-section">
                            <h3 className="filters-title">Дополнительные фильтры</h3>
                            <div className="additional-filters">
                                {additionalFilters.slice(0, 5).map((filter, index) => (
                                    <div key={index} className="additional-filter">
                                        {filter}
                                    </div>
                                ))}
                            </div>
                            <button className="show-more-btn">Показать ещё</button>
                        </div>

                        <div className="filters-section">
                            <h3 className="filters-title">Горячие темы</h3>
                            <div className="hot-topics">
                                {hotTopics.map((topic, index) => (
                                    <a key={index} href="#" className="hot-topic">
                                        {topic}
                                    </a>
                                ))}
                            </div>
                        </div>
                    </aside>
                    <main className="products-main">
                        <div className="products-header">
                            <div className="sorting-section">
                                <div className="sorting-controls">
                                    <span className="sorting-label">Сортировка: сначала популярные</span>
                                    <span className="grouping-label">Группировка: отсутствует</span>
                                </div>
                                <div className="header-actions">
                                    <div className="compare-section">
                                        <span className="compare-icon">↕</span>
                                        <span>Сравнить</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="products-list">
                            {products.map((product) => (
                                <div key={product.id} className="product-item">
                                    <div className="product-card">
                                        <div className="product-top-left">
                                            {product.isHit && (
                                                <div className="hit-badge">
                                                    Хит продаж
                                                </div>
                                            )}
                                            <div className="product-code-badge">
                                                {product.code}
                                            </div>
                                        </div>
                                        <div className="product-content">
                                            <div className="product-image-section">
                                                <div className="image-placeholder">
                                                    <div className="image-fallback">
                                                        Изображение
                                                    </div>
                                                </div>
                                            </div>
                                            <div className="product-info-section">
                                                <div className="product-header">
                                                    <h3 className="product-name">{product.name}</h3>
                                                    <p className="product-description">{product.description}</p>
                                                </div>

                                                <div className="availability-section">
                                                    <div className="availability-info">
                                                        {product.availability && (
                                                            <span className="availability-text">{product.availability}</span>
                                                        )}
                                                        {product.delivery && (
                                                            <span className="delivery-text">{product.delivery}</span>
                                                        )}
                                                        {product.pickup && (
                                                            <span className="pickup-text">{product.pickup}</span>
                                                        )}
                                                    </div>
                                                </div>

                                                <div className="product-footer">
                                                    <div className="product-meta">
                                                        <div className="rating-compare">
                                                            <div className="compare-badge">
                                                                <span className="compare-icon">↕</span>
                                                                <span>Сравнить</span>
                                                            </div>
                                                            <div className="rating-display">
                                                                <span className="star-icon">★</span>
                                                                <span className="rating-value">{product.rating}</span>
                                                                <span className="rating-separator">|</span>
                                                                <span className="reviews-count">{product.reviews} отзывов</span>
                                                            </div>
                                                            {product.reliability && (
                                                                <div className="reliability-badge">
                                                                    {product.reliability}
                                                                </div>
                                                            )}
                                                        </div>
                                                    </div>

                                                    <div className="product-pricing">
                                                        <div className="price-section">
                                                            <div className="price-main">{product.price}</div>
                                                            <div className="price-monthly">{product.monthlyPayment}</div>
                                                        </div>
                                                        <button className="buy-button">
                                                            Купить
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            ))}
                        </div>
                    </main>
                </div>
            </div>
        </>
    )
}

export default householdAppliances_pages
