import { useState } from 'react'
import './news_pages.sass'

function news_pages() {
    const [count, setCount] = useState(0)

    return (
        <>
            <main className='main'>
                <p className='main-title'>Новости</p>
                <div className="categories">
                    <span className="category active">Все</span>
                    <span className="category">Новинки</span>
                    <span className="category">Общая информация</span>
                    <span className="category">Эксклюзивы</span>
                    <span className="category">Магазины и сервисные центры</span>
                    <span className="category">Акция</span>
                    <span className="category">Услуги</span>
                </div>
                <div className="large-news-grid">
                    <div className="news_pages-content large">
                        <div className="highlight-title">
                            <h2>SHEO</h2>
                            <h1>ФЕН ДЛЯ ВОЛОС</h1>
                            <h3>WAITOMO*</h3>
                        </div>
                        <img className='news_pages-content-img' src="https://via.placeholder.com/400x300?text=Fen+Sheo+Waitomo" alt="Фен Sheo Waitomo" />
                        <div className="news-info">
                            <p className='news_pages-content-title'>Новинка! Фен Sheo Waitomo</p>
                            <p className='news_pages-content-text'>
                                Команда DNS представляет новинку — фен Sheo Waitomo. Новый фен Sheo Waitomo с насадкой для завивки — это решение для домашнего ухода с эффектом салонной укладки...
                            </p>
                            <div className="news-meta">
                                <span className="news-date">25.12.2025</span>
                                <span className="news-views">564 просмотра</span>
                            </div>
                        </div>
                    </div>
                    <div className="news_pages-content large">
                        <div className="highlight-title">
                            <h2>TURTLE BEACH</h2>
                            <h1>ИГРОВЫЕ НАУШНИКИ</h1>
                            <h3>STEALTH PRO</h3>
                        </div>
                        <img className='news_pages-content-img' src="https://via.placeholder.com/400x300?text=Turtle+Beach+Stealth+Pro" alt="Наушники Turtle Beach" />
                        <div className="news-info">
                            <p className='news_pages-content-title'>Новинка! Наушники Turtle Beach</p>
                            <p className='news_pages-content-text'>
                                Turtle Beach STEALTH PRO — многоплатформенная беспроводная игровая гарнитура, оснащена универсальной системой шумоподавления...
                            </p>
                            <div className="news-meta">
                                <span className="news-date">24.12.2025</span>
                                <span className="news-views">2213 просмотров</span>
                            </div>
                        </div>
                    </div>
                    <div className="news_pages-content large">
                        <div className="highlight-title">
                            <h2>FIERO</h2>
                            <h1>ПРОВОДНЫЕ НАУШНИКИ</h1>
                            <h3>MYTH</h3>
                        </div>
                        <img className='news_pages-content-img' src="https://via.placeholder.com/400x300?text=Fiero+Myth" alt="Наушники Fiero Myth" />
                        <div className="news-info">
                            <p className='news_pages-content-title'>Новинка! Проводные наушники Fiero Myth</p>
                            <p className='news_pages-content-text'>
                                Проводные охватывающие наушники Fiero Myth с удлинённым кабелем и мягким регулируемым оголовьем — решение для повседневного использования...
                            </p>
                            <div className="news-meta">
                                <span className="news-date">23.12.2025</span>
                                <span className="news-views">1689 просмотров</span>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="section-title">
                    <h2>Другие новинки</h2>
                </div>
                <div className="small-news-grid">
                    <div className="news_pages-content small">
                        <img className='news_pages-content-img' src="https://via.placeholder.com/300x200?text=Water+Heater" alt="Водонагреватель" />
                        <div className="news-info">
                            <p className='news_pages-content-title'>ПРОСТОЙ ВОДОНАГРЕВАТЕЛЬ HORIZON PRO</p>
                            <p className='news_pages-content-text'>Новая модель водонагревателя с улучшенной эффективностью...</p>
                            <div className="news-meta">
                                <span className="news-date">22.12.2025</span>
                                <span className="news-views">432 просмотра</span>
                            </div>
                        </div>
                    </div>

                    <div className="news_pages-content small">
                        <img className='news_pages-content-img' src="https://via.placeholder.com/300x200?text=Gaming+Mouse" alt="Игровая мышь" />
                        <div className="news-info">
                            <p className='news_pages-content-title'>Игровая мышь ABDAD GAMING Time Binary</p>
                            <p className='news_pages-content-text'>Высокоточная игровая мышь с настраиваемыми кнопками...</p>
                            <div className="news-meta">
                                <span className="news-date">21.12.2025</span>
                                <span className="news-views">789 просмотров</span>
                            </div>
                        </div>
                    </div>

                    <div className="news_pages-content small">
                        <img className='news_pages-content-img' src="https://via.placeholder.com/300x200?text=DEXP" alt="Френ Шотко" />
                        <div className="news-info">
                            <p className='news_pages-content-title'>Фаш-шёлка DEXP LIB. 8.6.5</p>
                            <p className='news_pages-content-text'>Новая версия программного обеспечения для умных устройств...</p>
                            <div className="news-meta">
                                <span className="news-date">20.12.2025</span>
                                <span className="news-views">321 просмотр</span>
                            </div>
                        </div>
                    </div>
                </div>
            </main>
        </>
    )
}

export default news_pages