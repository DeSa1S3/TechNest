import { useState } from 'react'
import './basket_pages.sass'

function basket_pages() {
    const [count, setCount] = useState(0)

    return (
        <>
            <main className='main'>
                <div className="main-container">
                    <div className="basket-layout">
                        {/* Основной блок с товаром - занимает всю ширину */}
                        <div className="product-section">
                            <div className="product-main">
                                <div className="product-card">
                                    <div className="product-info">
                                        <div className="product-title">
                                            <span className="product-category">Wi-Fi роутер</span>
                                            <h2 className="product-name">HUAWEI AX3 WS7100-25</h2>
                                        </div>
                                        <div className="product-meta">
                                            <div className="product-quantity-control">
                                                <button className="quantity-btn minus">−</button>
                                                <span className="quantity-value">1</span>
                                                <button className="quantity-btn plus">+</button>
                                            </div>
                                            <div className="product-code">Код: 5435209</div>
                                        </div>
                                    </div>

                                    <div className="product-availability">
                                        <span className="availability-text">В наличии: в 14 магазинах</span>
                                    </div>

                                    <div className="product-sections">
                                        <div className="section warranty-section">
                                            <h3 className="section-title">Доп. гарантия</h3>
                                            <div className="warranty-options">
                                                <div className="warranty-option selected">
                                                    <span className="option-text">Нет</span>
                                                </div>
                                                <div className="warranty-option">
                                                    <span className="option-text">+12 мес.</span>
                                                    <span className="option-price">390 ₽</span>
                                                </div>
                                                <div className="warranty-option">
                                                    <span className="option-text">+24 мес.</span>
                                                    <span className="option-price">540 ₽</span>
                                                </div>
                                            </div>
                                        </div>

                                        <div className="section accessories-section">
                                            <h3 className="section-title">Лучшие аксессуары</h3>
                                            <div className="accessories-grid">
                                                <div className="accessory-item">
                                                    <div className="accessory-price">399 ₽</div>
                                                </div>
                                                <div className="accessory-item">
                                                    <div className="accessory-price">1 399 ₽</div>
                                                </div>
                                                <div className="accessory-item">
                                                    <div className="accessory-price">320 ₽</div>
                                                </div>
                                                <div className="accessory-item">
                                                    <div className="accessory-price">9 799 ₽</div>
                                                </div>
                                                <div className="accessory-item">
                                                    <div className="accessory-price">699 ₽</div>
                                                </div>
                                                <div className="accessory-item">
                                                    <div className="accessory-price">6 499 ₽</div>
                                                </div>
                                                <div className="accessory-item">
                                                    <div className="accessory-price">699 ₽</div>
                                                </div>
                                                <div className="accessory-item">
                                                    <div className="accessory-price">3 099 ₽</div>
                                                </div>
                                                <div className="accessory-item">
                                                    <div className="accessory-price">150 ₽</div>
                                                </div>
                                                <div className="accessory-item">
                                                    <div className="accessory-price">9 399 ₽</div>
                                                </div>
                                            </div>
                                        </div>

                                        <div className="section terms-section">
                                            <h3 className="section-title">Условия заказа</h3>
                                            <div className="terms-content">
                                                <label className="checkbox-label">
                                                    <input type="checkbox" />
                                                    <span>Скрыть витринные образцы</span>
                                                </label>
                                                <div className="terms-note">
                                                    условия получения заказа могут измениться
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="sidebar-section">
                            <div className="summary-card">
                                <div className="summary-header">
                                    <h3 className="summary-title">Итого:</h3>
                                </div>
                                <div className="summary-content">
                                    <div className="summary-row">
                                        <span className="row-label">1 товар</span>
                                        <span className="row-value">2 999 ₽</span>
                                    </div>
                                    <div className="summary-total">
                                        <span className="total-label">К оплате:</span>
                                        <span className="total-value">2 999 ₽</span>
                                    </div>
                                    <button className="checkout-btn">Перейти к оформлению</button>
                                </div>
                            </div>

                            <div className="delivery-card">
                                <div className="delivery-item">
                                    <div className="delivery-icon">●</div>
                                    <span className="delivery-text">В наличии: в 14 магазинах</span>
                                </div>
                                <div className="delivery-item">
                                    <div className="delivery-icon">●</div>
                                    <span className="delivery-text">Доставим на дом: Завтра</span>
                                </div>
                                <div className="delivery-item">
                                    <div className="delivery-icon">●</div>
                                    <span className="delivery-text">Пункты доступ</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </main>
        </>
    )
}

export default basket_pages