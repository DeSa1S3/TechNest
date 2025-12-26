import { useState } from 'react'
import './order_pages.sass'

function order_pages() {
    const [count, setCount] = useState(0)

    return (
        <>
            <main className='main'>
                <div className="main-container">
                    <div className="main-order-details">
                        <h2 className="order-title">Детали заказа</h2>
                        <div className="order-info">
                            <div className="info-row">
                                <span className="info-label">Номер заказа:</span>
                                <span className="info-value">#245678</span>
                            </div>
                            <div className="info-row">
                                <span className="info-label">Способ получения:</span>
                                <span className="info-value">Самовывоз</span>
                            </div>
                            <div className="info-row">
                                <span className="info-label">Ожидаемый срок получения:</span>
                                <span className="info-value">28 декабря 2025</span>
                            </div>
                            <div className="info-row">
                                <span className="info-label">Срок хранения заказа:</span>
                                <span className="info-value">3 дня</span>
                            </div>
                            <div className="info-row">
                                <span className="info-label">Оплата заказа:</span>
                                <span className="info-value">Картой онлайн</span>
                            </div>
                        </div>
                    </div>
                    <div className="main-order-items">
                        <h2 className="order-title">Состав заказа</h2>
                        <div className="order-items-list">
                            <div className="order-item">
                                <div className="item-image">
                                    <div className="image-placeholder">🛍️</div>
                                </div>
                                <div className="item-details">
                                    <h3 className="item-name">Фен Sheo Waitomo</h3>
                                    <p className="item-description">Фен для волос с насадкой для завивки</p>
                                </div>
                                <div className="item-price">
                                    <span className="price">$41</span>
                                </div>
                            </div>

                            <div className="order-item">
                                <div className="item-image">
                                    <div className="image-placeholder">🎧</div>
                                </div>
                                <div className="item-details">
                                    <h3 className="item-name">Наушники Turtle Beach</h3>
                                    <p className="item-description">Игровая гарнитура с шумоподавлением</p>
                                </div>
                                <div className="item-price">
                                    <span className="price">$2198</span>
                                </div>
                            </div>

                            <div className="order-item">
                                <div className="item-image">
                                    <div className="image-placeholder">🎵</div>
                                </div>
                                <div className="item-details">
                                    <h3 className="item-name">Наушники Fiero Myth</h3>
                                    <p className="item-description">Проводные охватывающие наушники</p>
                                </div>
                                <div className="item-price">
                                    <span className="price">$1684</span>
                                </div>
                            </div>
                        </div>

                        <div className="order-total">
                            <div className="total-row">
                                <span className="total-label">Итого:</span>
                                <span className="total-value">$3923</span>
                            </div>
                        </div>
                    </div>
                </div>
            </main>
        </>
    )
}

export default order_pages
