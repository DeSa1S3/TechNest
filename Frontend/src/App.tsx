import './App.sass'
import { Routes, Route } from 'react-router-dom'
import Layout from './components/layout'
import MainPages from './main_pages/main_pages'
import FavoritesPages from './favorites_pages/favorites_pages'
import Basket_pages from './basket_pages/basket_pages'
import Profile from './personal-account_pages/personal-account_pages'
import Catalog from './catalog_pages/catalog_pages'
import News from './news_pages/news_pages'
import Order from './order_pages/order_pages'
import BeautyandHealth from './catalog-items/beauty-and-health/beauty-and-heath'
import HouseHoldApliances from './catalog-items/household-appliances/household-appliances'
import PCsLaptopsPeripherals from './catalog-items/PCs-laptops-peripherals/PCs-laptops-peripherals'
import SmartphonesandPhotographicEquiment from './catalog-items/smartphones-and-photographic-equipment/smartphones-and-photographic-equipment'
import TVConsolesandAudio from './catalog-items/TV-сonsoles-and-audio/TV-сonsoles-and-audio'
import Auth from './auth-and-register/auth'
import Register from './auth-and-register/register'


function App() {
  return (
    <Routes>
      <Route path="/" element={<Layout><MainPages /></Layout>} />
      <Route path="/auth" element={<Layout><Auth /></Layout>} />
      <Route path="/register" element={<Layout><Register /></Layout>} />
      <Route path="/favorites" element={<Layout><FavoritesPages /></Layout>} />
      <Route path="/basket" element={<Layout><Basket_pages /></Layout>} />
      <Route path="/profile" element={<Layout><Profile /></Layout>} />
      <Route path="/catalog" element={<Layout><Catalog /></Layout>} />
      <Route path="/news" element={<Layout><News /></Layout>} />
      <Route path="/order" element={<Layout><Order /></Layout>} />
      <Route path="/beautyandhealth" element={<Layout><BeautyandHealth /></Layout>} />
      <Route path="/householdappliances" element={<Layout><HouseHoldApliances /></Layout>} />
      <Route path="/PCslaptopsperiphearls" element={<Layout><PCsLaptopsPeripherals /></Layout>} />
      <Route path="/SmartphonesandPhotographicEquiment" element={<Layout><SmartphonesandPhotographicEquiment /></Layout>} />
      <Route path="/TVconsolesandAudio" element={<Layout><TVConsolesandAudio /></Layout>} />
    </Routes>
  )
}

export default App