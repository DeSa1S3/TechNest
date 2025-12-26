import { useState } from 'react'
import '../favorites_pages/favorites.sass'

interface Product {
    id: number
    name: string
    description: string
    originalPrice: number
    currentPrice: number
    monthlyPayment: number
    discount: string
    features: string[]
    rating: number
    reviewsCount: string
    reliability: string
    status: string
    color: string
    imageBg: string
}

function FavoritesPages() {
    const [selectedProducts, setSelectedProducts] = useState<number[]>([])
    const [selectAll, setSelectAll] = useState(false)

    const products: Product[] = [
        {
            id: 1,
            name: '15.6" Ноутбук Apple MacBook Air черный',
            description: '[английская/русская раскладка, 2560x1664, IPS, Apple M2 8-core, ядра: 4 + 4, RAM 8 ГБ, SSD 256 ГБ, Apple M2 8-core, macOS]',
            originalPrice: 88999,
            currentPrice: 80799,
            monthlyPayment: 7417,
            discount: 'Выгода 8 200 ₽',
            features: ['Рассрочка 0-0-12', 'Выгодные комплекты'],
            rating: 4.25,
            reviewsCount: '17 отзывов',
            reliability: 'Отличная надежность',
            status: 'Товара нет в наличии',
            color: 'черный',
            imageBg: '#1a1a1a'
        },
        {
            id: 2,
            name: '13.3" Ноутбук Apple MacBook Air серый',
            description: '[английская/русская раскладка, 2560x1600, IPS, Apple M1, ядра: 4 + 4 x 3.2 ГГц + 2.1 ГГц, RAM 8 ГБ, SSD 256 ГБ, Apple M1 7-core, macOS]',
            originalPrice: 72499,
            currentPrice: 69799,
            monthlyPayment: 6042,
            discount: 'Выгода 2 700 ₽',
            features: ['Рассрочка 0-0-12', 'Выгодные комплекты', 'Витринный образец'],
            rating: 4.87,
            reviewsCount: '1.1k отзывов',
            reliability: 'Отличная надежность',
            status: '',
            color: 'серый',
            imageBg: '#808080'
        }
    ]

    const totalAmount = products.reduce((sum, product) => sum + product.currentPrice, 0)
    const selectedAmount = selectedProducts.reduce((sum, id) => {
        const product = products.find(p => p.id === id)
        return sum + (product?.currentPrice || 0)
    }, 0)

    const toggleSelectAll = () => {
        if (selectAll) {
            setSelectedProducts([])
        } else {
            setSelectedProducts(products.map(p => p.id))
        }
        setSelectAll(!selectAll)
    }

    const toggleProduct = (id: number) => {
        if (selectedProducts.includes(id)) {
            setSelectedProducts(selectedProducts.filter(productId => productId !== id))
        } else {
            setSelectedProducts([...selectedProducts, id])
        }
    }


    return (
        <>
            <div className="root">
                <main className='main'>
                    <div className="main-content">
                        <h1 className='main-title'>Избранное</h1>

                        <div className="main-profile-wishlist-managment">
                            <div className="selection-area">
                                <label className="select-all-checkbox">
                                    <input
                                        type="checkbox"
                                        checked={selectAll}
                                        onChange={toggleSelectAll}
                                    />
                                    Выбрать все
                                </label>
                                <p className='main-profile-wishlist-managment-text'>
                                    {selectedProducts.length > 0
                                        ? `${selectedProducts.length} товара на сумму: ${selectedAmount.toLocaleString('ru-RU')} ₽`
                                        : `${products.length} товара на сумму: ${totalAmount.toLocaleString('ru-RU')} ₽`
                                    }
                                </p>
                            </div>
                            <button className='main-profile-wishlist-managment-btn'>
                                Купить
                            </button>
                        </div>

                        <div className="catalog-product">
                            {products.map((product) => (
                                <div key={product.id} className="product-item">
                                    <div className="product-checkbox">
                                        <input
                                            type="checkbox"
                                            checked={selectedProducts.includes(product.id)}
                                            onChange={() => toggleProduct(product.id)}
                                        />
                                    </div>

                                    <div
                                        className="product-image-container"
                                        style={{ backgroundColor: product.imageBg }}
                                    >
                                        <span>{product.color}</span>
                                    </div>

                                    <div className="product-details">
                                        <h3 className="product-name">{product.name}</h3>
                                        <p className="product-description">{product.description}</p>

                                        <div className="product-features">
                                            {product.features.map((feature, index) => (
                                                <span key={index} className="product-feature">{feature}</span>
                                            ))}
                                        </div>

                                        <div className="product-rating-section">
                                            <button className="compare-btn">Сравнить</button>
                                            <div className="product-rating">
                                                <span className="rating-star">✦</span>
                                                <span className="rating-value">{product.rating}</span>
                                                <span className="reviews-count"> | {product.reviewsCount}</span>
                                            </div>
                                        </div>

                                        <div className="product-reliability">{product.reliability}</div>

                                        {product.status && (
                                            <div className="product-status">{product.status}</div>
                                        )}

                                        <div className="product-actions">
                                            <button className="notify-btn">Уведомить</button>
                                            <button className="show-analogs-btn">Показать аналоги</button>
                                        </div>
                                    </div>

                                    <div className="product-prices">
                                        <div className="price-container">
                                            <span className="original-price">
                                                {product.originalPrice.toLocaleString('ru-RU')} ₽
                                            </span>
                                            <span className="current-price">
                                                {product.currentPrice.toLocaleString('ru-RU')} ₽
                                            </span>
                                            <span className="monthly-payment">
                                                или {product.monthlyPayment.toLocaleString('ru-RU')} ₽/мес.
                                            </span>
                                        </div>

                                        {product.status ? (
                                            <button className="notify-btn" style={{ width: '100%' }}>
                                                Уведомить
                                            </button>
                                        ) : (
                                            <button className="buy-btn">
                                                Купить
                                            </button>
                                        )}
                                    </div>
                                </div>
                            ))}
                        </div>
                    </div>
                </main>
            </div>
        </>
    )
}

export default FavoritesPages