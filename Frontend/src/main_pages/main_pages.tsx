import { useState } from 'react'
import './main_pages.sass'
import sumsung_logo from '../../public/main_pages/sumsung_logo.svg'
import honor_logo from '../../public/main_pages/honor_logo.png'
import hair_logo from '../../public/main_pages/hair_logo.png'
import apple_logo from '../../public/main_pages/Apple_logo.png'
import ardor_logo from '../../public/main_pages/ardor_logo.png'
import mi_logo from '../../public/main_pages/mi_logo.png'
import dexp_logo from '../../public/main_pages/dexp_logo.png'
import img_story_1 from '../../public/main_pages/img-story-1.jpg'
import img_story_2 from '../../public/main_pages/img-story-2.jpg'
import img_story_3 from '../../public/main_pages/img-story-3.jpg'
import img_story_4 from '../../public/main_pages/img-storty-4.jpg'
import img_story_5 from '../../public/main_pages/img-story-5.jpg'
import img_story_6 from '../../public/main_pages/img-storty-6.jpg'

function MainPages() {
    const [count, setCount] = useState(0)
    return (
        <>
            <div className="root">
                <main className='main'>
                    <div className="main-selection">
                        <div className="main-selection-all">
                            <button className='main-selection-all-btn'>Все</button>
                        </div>
                        <div className="main-selection-company">
                            <button className='main-selection-company-btn'>Компании</button>
                        </div>
                        <div className="main-selection-news">
                            <button className='main-selection-news-btn'>Новости</button>
                        </div>
                        <div className="main-selection-like_it">
                            <button className='main-selection-like_it-btn'>Возможно вам понравится</button>
                        </div>
                    </div>
                    <div className="main-all">
                        <div className="main-all-company">
                            <div className="main-all-company-items">
                                <img className='main-all-company-items-img' src={sumsung_logo} alt="" />
                            </div>
                            <div className="main-all-company-items">
                                <img className='main-all-company-items-img' src={honor_logo} alt="" />
                            </div>
                            <div className="main-all-company-items">
                                <img className='main-all-company-items-img' src={hair_logo} alt="" />
                            </div>
                            <div className="main-all-company-items">
                                <img className='main-all-company-items-img' src={apple_logo} alt="" />
                            </div>
                            <div className="main-all-company-items">
                                <img className='main-all-company-items-img' src={ardor_logo} alt="" />
                            </div>
                            <div className="main-all-company-items">
                                <img className='main-all-company-items-img' src={mi_logo} alt="" />
                            </div>
                            <div className="main-all-company-items">
                                <img className='main-all-company-items-img' src={dexp_logo} alt="" />
                            </div>
                        </div>
                        <div className="main-all-story">
                            <div className="story-block">
                                <div className="main-all-story_top">
                                    <img className='main-all-story-img' src={img_story_1} alt="" />
                                    <p className='main-all-story-text'>Новинка! Водонагреватели DEXP HORIZON PRO 60л</p>
                                </div>
                                <div className="main-all-story_top">
                                    <img className='main-all-story-img' src={img_story_2} alt="" />
                                    <p className='main-all-story-text'>Новинка! Ноутбук MAIBENBEN X16 B стиле Atomic Heart</p>
                                </div>
                            </div>
                            <div className="story-block">
                                <div className="main-all-story_top vertical">
                                    <img className='main-all-story-img' src={img_story_3} alt="" />
                                    <p className='main-all-story-text'>Новичка! Наушники Turtle Beach</p>
                                </div>
                                <div className="main-all-story_top vertical">
                                    <img className='main-all-story-img' src={img_story_4} alt="" />
                                    <p className='main-all-story-text'>Новичка! Проводные наушники Fiero Myth</p>
                                </div>
                                <div className="main-all-story_top vertical">
                                    <img className='main-all-story-img' src={img_story_5} alt="" />
                                    <p className='main-all-story-text'>Новичка! Коврики ARDOR GAMING Tiny Bunny</p>
                                </div>
                                <div className="main-all-story_top vertical">
                                    <img className='main-all-story-img' src={img_story_6} alt="" />
                                    <p className='main-all-story-text'>Новичка! Фон-чайтка DEXP H8-865</p>
                                </div>
                            </div>
                        </div>
                        <div className="main-all-you_might_like_it">
                            <h2 className="section-title">Вам может понравиться</h2>
                            <div className="products-grid">
                                <div className="main-all-you_might_like_it_top product-card">
                                    <div className="image-container">
                                        <img className='main-all-you_might_like_it_top-img' src="" alt="" />
                                    </div>
                                    <div className="price-container">
                                        <span className="current-price">60 499 ₽</span>
                                        <span className="old-price">64,398 ₽</span>
                                    </div>
                                    <p className='main-all-you_might_like_it_top_text'>
                                        6.1" Смартфон Apple iPhone 15 128 ГБ черный [ядер - 6x(5,46 ГГ...)]
                                    </p>
                                    <p className='main-all-you_might_like_it_top-rating'>
                                        ✔ 4.8 | 3000 отзывов
                                    </p>
                                </div>
                                <div className="main-all-you_might_like_it_top product-card">
                                    <div className="image-container">
                                        <img className='main-all-you_might_like_it_top-img' src="" alt="" />
                                    </div>
                                    <div className="price-container">
                                        <span className="current-price">1 150 ₽</span>
                                    </div>
                                    <p className='main-all-you_might_like_it_top_text'>
                                        Коврик ARDOR GAMING Tiny Bunny Alisa Dream (XL) черный...
                                    </p>
                                    <p className='main-all-you_might_like_it_top-rating'>
                                        ✔ 5 | 22 отзыва
                                    </p>
                                </div>
                                <div className="main-all-you_might_like_it_top product-card">
                                    <div className="image-container">
                                        <img className='main-all-you_might_like_it_top-img' src="" alt="" />
                                    </div>
                                    <div className="price-container">
                                        <span className="current-price">12 499 ₽</span>
                                    </div>
                                    <p className='main-all-you_might_like_it_top_text'>
                                        Аэрогриль Xiaomi Dual Zone Air Fryer 10L серый [2700 Вт, 10 л,...]
                                    </p>
                                    <p className='main-all-you_might_like_it_top-rating'>
                                        ✔ 4.8 | 31 отзыв
                                    </p>
                                </div>
                                <div className="main-all-you_might_like_it_top product-card">
                                    <div className="image-container">
                                        <img className='main-all-you_might_like_it_top-img' src="" alt="" />
                                    </div>
                                    <div className="price-container">
                                        <span className="current-price">15 ₽</span>
                                    </div>
                                    <p className='main-all-you_might_like_it_top_text'>
                                        Эко-пакет Крафтыряхет MAPK [крафт-бумага, 35 см x 45 см x 1...]
                                    </p>
                                    <p className='main-all-you_might_like_it_top-rating'>
                                        ✔ 4.8 | 167 отзывов
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </main>
            </div>
        </>
    )
}

export default MainPages
