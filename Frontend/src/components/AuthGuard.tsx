import React, { useEffect, useState } from 'react';
import { Navigate } from 'react-router-dom';
import { authService } from '../service/apiServices';

interface AuthGuardProps {
    children: React.ReactNode;
    requiredRole?: string;
}

const AuthGuard: React.FC<AuthGuardProps> = ({ children, requiredRole }) => {
    const [isAuthenticated, setIsAuthenticated] = useState<boolean | null>(null);
    const [userRole, setUserRole] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        checkAuth();
    }, []);

    const checkAuth = async () => {
        try {
            const isValid = await authService.validateToken();
            setIsAuthenticated(isValid);

            if (isValid) {
                const token = localStorage.getItem('access_token');
                if (token) {
                    try {
                        const payload = JSON.parse(atob(token.split('.')[1]));
                        const roles = payload.Role ? JSON.parse(payload.Role) : ['USER'];
                        setUserRole(roles[0] || 'USER');
                    } catch {
                        setUserRole('USER');
                    }
                }
            }
        } catch {
            setIsAuthenticated(false);
        } finally {
            setIsLoading(false);
        }
    };

    if (isLoading) {
        return (
            <div style={{
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
                height: '100vh'
            }}>
                <div>Проверка авторизации...</div>
            </div>
        );
    }

    if (!isAuthenticated) {
        return <Navigate to="/auth" replace />;
    }

    if (requiredRole && userRole !== requiredRole) {
        return <Navigate to="/" replace />;
    }

    return <>{children}</>;
};

export default AuthGuard;