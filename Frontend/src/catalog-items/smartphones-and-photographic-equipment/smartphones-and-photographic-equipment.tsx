import { useState } from 'react'
import './smartphones-and-photographic-equipment.sass'

function SmartphonesAndPhotographicEquiment_pages() {
    const [count, setCount] = useState(0)

    const products = [
        {
            id: 1,
            name: "6.3 Смартфон Apple iPhone 17 Pro 256 ГБ синий",
            description: "[6.3, 2622x1206, Super Retina XDR, 120 Гц]",
            price: "132 499 ₽",
            monthlyPayment: "от 6 521 ₽/мес.",
            rating: 4.79,
            reviews: "234",
            availability: "В наличии в 7 магазинах",
            code: "5475586",
            isHit: false
        },
        {
            id: 2,
            name: "6.3 Смартфон Apple iPhone 17 Pro 256 ГБ серебристый",
            description: "[6.3, 2622x1206, Super Retina XDR, 120 Гц]",
            price: "132 999 ₽",
            monthlyPayment: "от 6 521 ₽/мес.",
            rating: 4.79,
            reviews: "234",
            reliability: "Отличная надежность",
            code: "5475587",
            isHit: false
        },
        {
            id: 3,
            name: "6.3 Смартфон Apple iPhone 17 256 ГБ черный",
            description: "[6.9, 2868x1320, Super Retina XDR, 120 Гц]",
            price: "127 999 ₽",
            monthlyPayment: "от 6 128 ₽/мес.",
            rating: 4.76,
            reviews: "1.1k",
            reliability: "Отличная надежность",
            code: "1247270",
            isHit: true,
            availability: "В наличии в 12 магазинах",
            delivery: "Доставим на дом сегодня",
            pickup: "Пункты выдачи доступны"
        },
        {
            id: 4,
            name: "6.7 Смартфон Samsung Galaxy S25 FE 256 ГБ голубой",
            description: "[6.7, 2340x1080, Dynamic AMOLED 2X, 120 Гц]",
            price: "54 499 ₽",
            monthlyPayment: "от 1 790 ₽/мес.",
            rating: 4.86,
            reviews: "642",
            reliability: "Отличная надежность",
            delivery: "Доставим на дом послезавтра",
            code: "5475589",
            isHit: false
        }
    ]

    const additionalFilters = [
        "Объем встроенной памяти",
        "Объем оперативной памяти",
        "Операционная система",
        "Модель",
        "Год релиза",
        "Емкость аккумулятора",
        "Диагональ экрана",
        "NFC",
        "Частота обновления экрана",
        "Количество ядер"
    ]

    const hotTopics = [
        "Что нужно для качественной съемки на смартфон",
        "Новый смартфон среднего уровня или старый флагмен?",
        "10 лучших камерофонов",
        "Как выбрать смартфон"
    ]
    return (
        <>
            <div className="beauty-health-container">
                <header className="page-header">
                    <h1 className="page-title">Смартфоны и фототехника</h1>
                </header>

                <div className="content-wrapper">
                    <aside className="filters-sidebar">
                        <div className="filters-section">
                            <h3 className="filters-title">Поиск по фильтрам</h3>
                            <div className="filter-options">
                                <label className="filter-option">
                                    <input type="checkbox" />
                                    <span className="checkmark"></span>
                                    Android
                                </label>
                                <label className="filter-option">
                                    <input type="checkbox" />
                                    <span className="checkmark"></span>
                                    IOS
                                </label>
                                <label className="filter-option">
                                    <input type="checkbox" />
                                    <span className="checkmark"></span>
                                    2025 года
                                </label>
                                <label className="filter-option">
                                    <input type="checkbox" />
                                    <span className="checkmark"></span>
                                    256 ГБ ПЗУ
                                </label>
                                <label className="filter-option">
                                    <input type="checkbox" />
                                    <span className="checkmark"></span>
                                    С беспроводной зарядкой
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
                                <p>В любом из 13 магазинов</p>
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

export default SmartphonesAndPhotographicEquiment_pages
