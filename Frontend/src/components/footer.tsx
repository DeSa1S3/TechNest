import { useState, useEffect } from 'react'
// import { useNavigate } from 'react-router-dom';

const Footer: React.FC = () => {
    // const navigate = useNavigate();

    return (<>
        <footer className='footer'>
            <div className="footer-logo">
                <h1 className='footer-brand'>TechNest</h1>
            </div>

            <div className="footer-columns">

                <div className="footer-column">
                    <h3 className="footer-column-title">Компания</h3>
                    <ul className="footer-list">
                        <li><a href="#" className='footer-text'>О компании</a></li>
                        <li><a href="#" className='footer-text'>Новости</a></li>
                        <li><a href="#" className='footer-text'>Партнерам (аренда, сотрудничество)</a></li>
                        <li><a href="#" className='footer-text'>Вакансии</a></li>
                        <li><a href="#" className='footer-text'>Политика конфиденциальности</a></li>
                        <li><a href="#" className='footer-text'>Персональные данные</a></li>
                        <li><a href="#" className='footer-text'>Правила продаж</a></li>
                        <li><a href="#" className='footer-text'>Правила пользования сайта</a></li>
                    </ul>
                </div>

                <div className="footer-column">
                    <h3 className="footer-column-title">Покупателям</h3>
                    <ul className="footer-list">
                        <li><a href="#" className='footer-text'>Как оформить заказ</a></li>
                        <li><a href="#" className='footer-text'>Способы оплаты</a></li>
                        <li><a href="#" className='footer-text'>Кредиты</a></li>
                        <li><a href="#" className='footer-text'>Доставка</a></li>
                        <li><a href="#" className='footer-text'>Статус заказа</a></li>
                        <li><a href="#" className='footer-text'>Обмен, возврат, гарантия</a></li>
                        <li><a href="#" className='footer-text'>Проверка статуса ремонта</a></li>
                    </ul>
                </div>

                <div className="footer-column">
                    <h3 className="footer-column-title">Юридическим лицам</h3>
                    <ul className="footer-list">
                        <li><a href="#" className='footer-text'>Проверка счета</a></li>
                        <li><a href="#" className='footer-text'>Корпоративные отделы</a></li>
                        <li><a href="#" className='footer-text'>Подарочные карты</a></li>
                        <li><a href="#" className='footer-text'>Бонусная программа</a></li>
                        <li><a href="#" className='footer-text'>Помощь</a></li>
                        <li><a href="#" className='footer-text'>Обратная связь</a></li>
                    </ul>
                </div>

                <div className="footer-column">
                    <h3 className="footer-column-title">TechNest (звонок по России)</h3>
                    <div className="footer-contact">
                        <p className="footer-contact-text">Оставайтесь на связи</p>
                        <a href="tel:88007707999" className="footer-phone">8-800-77-07-999</a>
                        <p className="footer-hours">(с 04:00 до 23:00)</p>
                        <p className="footer-address">Адреса магазинов в г. Ульяновск</p>
                    </div>
                </div>

            </div>

            <div className="footer-bottom">
                <p className="footer-bottom-text">TechNest всегда под рукой</p>
            </div>
        </footer>
    </>)
}
export default Footer