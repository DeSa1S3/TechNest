import Header from './header'
import Footer from './footer'

interface LayoutProps {
    children: React.ReactNode;
}

const Layout = ({ children }: LayoutProps) => {
    return (
        <div className="root">
            <Header />
            <main className='main'>
                {children}
            </main>
            <Footer />
        </div>
    )
}

export default Layout