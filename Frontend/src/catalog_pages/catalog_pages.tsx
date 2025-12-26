import { useState } from 'react'
import './catalog_pages.sass'
import { Link } from 'react-router-dom'

function CatalogPages() {
    const [count, setCount] = useState(0)

    return (
        <div className="catalog-container">
            <main className='catalog-main'>
                <h1 className="main-title">Каталог товаров</h1>

                <div className="categories-row first-row">
                    <div className="category-card">
                        <Link to="/householdappliances">
                            <div className="category-content">
                                <div className="category-icon">
                                    <img src="" alt="Бытовая техника" />
                                </div>
                                <p className="category-name">Бытовая техника</p>
                            </div>
                        </Link>

                    </div>

                    <div className="category-card">
                        <Link to="/beautyandhealth">
                            <div className="category-content">
                                <div className="category-icon">
                                    <img src="" alt="Красота и здоровье" />
                                </div>
                                <p className="category-name">Красота и здоровье</p>
                            </div>
                        </Link>
                    </div>

                    <div className="category-card">
                        <Link to="/SmartphonesandPhotographicEquiment">
                            <div className="category-content">
                                <div className="category-icon">
                                    <img src="" alt="Смартфоны и фототехника" />
                                </div>
                                <p className="category-name">Смартфоны и фототехника</p>
                            </div>
                        </Link>
                    </div>

                    <div className="category-card">
                        <Link to="/TVconsolesandAudio">
                            <div className="category-content">
                                <div className="category-icon">
                                    <img src="" alt="ТВ, консоли и аудио" />
                                </div>
                                <p className="category-name">ТВ, консоли и аудио</p>
                            </div>
                        </Link>
                    </div>
                </div>

                <div className="categories-row second-row">
                    <Link to="/PCslaptopsperiphearls">
                        <div className="category-card">
                            <div className="category-content">
                                <div className="category-icon">
                                    <img src="" alt="ПК, ноутбуки, периферия" />
                                </div>
                                <p className="category-name">ПК, ноутбуки, периферия</p>
                            </div>
                        </div>
                    </Link>

                    <div className="category-card">
                        <div className="category-content">
                            <div className="category-icon">
                                <img src="" alt="Ноутбуки и аксессуары" />
                            </div>
                            <p className="category-name">Ноутбуки и аксессуары</p>
                        </div>
                    </div>

                    <div className="category-card">
                        <div className="category-content">
                            <div className="category-icon">
                                <img src="" alt="Компьютеры и ПО" />
                            </div>
                            <p className="category-name">Компьютеры и ПО</p>
                        </div>
                    </div>

                    <div className="category-card">
                        <div className="category-content">
                            <div className="category-icon">
                                <img src="" alt="Периферия и аксессуары" />
                            </div>
                            <p className="category-name">Периферия и аксессуары</p>
                        </div>
                    </div>
                </div>

                <div className="categories-row third-row">
                    <div className="category-card half-width">
                        <div className="category-content">
                            <div className="category-icon">
                                <img src="" alt="Собрать ПК" />
                            </div>
                            <p className="category-name">Собрать ПК</p>
                        </div>
                    </div>

                    <div className="category-card half-width">
                        <div className="category-content">
                            <div className="category-icon">
                                <img src="" alt="Готовые сборки" />
                            </div>
                            <p className="category-name">Готовые сборки</p>
                        </div>
                    </div>
                </div>

                <div className="additional-sections">
                    <div className="left-column">
                        <div className="subcategory-section">
                            <h3 className="subcategory-title">ПК, ноутбуки, периферия</h3>
                            <ul className="subcategory-list">
                                <li>Комплектующие для ПК</li>
                            </ul>
                        </div>

                        <div className="subcategory-section">
                            <h3 className="subcategory-title">Смартфоны и фототехника</h3>
                            <ul className="subcategory-list">
                                <li>Смартфоны и гаджеты</li>
                                <li>Планшеты, электронные книги</li>
                                <li>Фототехника</li>
                            </ul>
                        </div>
                    </div>

                    <div className="right-column">
                        <div className="subcategory-section">
                            <h3 className="subcategory-title">ТВ, консоли и аудио</h3>
                            <h4 className="network-title">Сетевое оборудование</h4>
                            <ul className="subcategory-list">
                                <li>Wi-Fi роутеры</li>
                                <li>Оборудование для малых сетей</li>
                                <li>Видеонаблюдение</li>
                                <li>Профессиональное сетевое оборудование</li>
                                <li>Защита и элементы питания</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </main>
        </div>
    )
}

export default CatalogPages