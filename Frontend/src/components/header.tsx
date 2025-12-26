import { useState, useEffect } from 'react'
import profile from '../../public/main_pages/profile.png'
import basket from '../../public/main_pages/basket.png'
import favorites from '../../public/main_pages/favorites.png'
import { Link } from 'react-router-dom';

const Header: React.FC = () => {

    const [Auth, setAuth] = useState<boolean>(false)

    return (
        <>
            <header className='header'>
                <div className="header-catalog">
                    <Link to="/" className='header-catalog-link'>
                        <h1 className='header-catalog-title'>TechNest</h1>
                    </Link>
                    <Link to="/catalog" className='header-catalog-link'>
                        <button className='header-catalog-btn'>Каталог</button>
                    </Link>
                </div>
                <div className="header-search">
                    <input type="text" placeholder='Поиск по сайту' className='header-search-input' />
                </div>
                <div className="header-nav">
                    <Link to="/favorites" className="header-nav-favorites">
                        <img src={favorites} alt="Избранное" className='header-nav-favorites-img' />
                        <p className='header-nav-favorites-text'>Избранное</p>
                    </Link>
                    <Link to="/basket" className='header-nav-basket'>
                        <img src={basket} alt="Корзина" className='header-nav-basket-img' />
                        <p className='header-nav-basket-text'>Корзина</p>
                    </Link>
                    <Link to="/profile" className="header-nav-profile">
                        <img src={profile} alt="Профиль" className='header-nav-profile-img' />
                    </Link>
                </div>
            </header>
        </>)
}
export default Header