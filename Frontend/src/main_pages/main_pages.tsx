import { useState } from 'react'
import './main_pages.sass'

function App() {
    const [count, setCount] = useState(0)

    return (
        <>
            <header>
                <div className="header-catalog">
                    <h1 className='header-catalog-title'>TechNest</h1>
                    <button className='header-catalog-btn'>Каталог</button>
                </div>
                <div className="header-search">
                    <input type="text" className='header=search-input' />
                </div>
                <div className="header-nav">
                    <div className="header-nav-favorites">
                        <img src="" className='header-nav-favorites-img' alt="" />
                        <p className='header-nav-favorites-text'>Избранное</p>
                    </div>
                    <div className="header-nav-basket">
                        <img src="" className='header-nav-basket-img' alt="" />
                        <p className='header-nav-basket-text'>Избранное</p>
                    </div>
                    <div className="header-nav-profile">
                        <img src="" className='header-nav-profile-img' alt="" />
                    </div>
                </div>
            </header>
            <main>
                <div className="main-selection">
                    <div className="main-selection-all">
                        <button className='main-selection-all-btn'></button>
                    </div>
                    <div className="main-selection-news">
                        <button className='main-selection-news-btn'></button>
                    </div>
                    <div className="main-selection-like_it">
                        <button className='main-selection-like_it-btn'></button>
                    </div>
                </div>
                <div className="main-all">

                </div>
            </main>
        </>
    )
}

export default App
