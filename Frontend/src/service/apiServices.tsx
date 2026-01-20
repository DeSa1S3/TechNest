const API_BASE_URL = 'http://localhost:5000/api';

const getAuthHeaders = (): Record<string, string> => {
    const token = localStorage.getItem('access_token');
    const headers: Record<string, string> = {
        'Content-Type': 'application/json'
    };

    if (token) {
        headers['Authorization'] = `Bearer ${token}`;
    }

    return headers;
};

const handleResponse = async (response: Response) => {
    if (!response.ok) {
        try {
            const errorData = await response.json();
            throw new Error(errorData.message || errorData.title || `Ошибка HTTP: ${response.status}`);
        } catch {
            throw new Error(`Ошибка HTTP: ${response.status}`);
        }
    }

    if (response.status === 204) {
        return null;
    }

    return response.json();
};

const fetchWithAuth = async (url: string, options: RequestInit = {}) => {
    const headers = getAuthHeaders();

    const config: RequestInit = {
        ...options,
        headers: {
            ...headers,
            ...options.headers,
        }
    };

    try {
        const response = await fetch(url, config);
        return await handleResponse(response);
    } catch (error: any) {
        console.error('API Error:', error);
        throw error;
    }
};

export const authService = {
    async login(email: string, password: string): Promise<any> {
        const response = await fetch(`${API_BASE_URL}/Auth/Login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ Email: email, Password: password })
        });

        const data = await handleResponse(response);

        if (data.accessToken) {
            localStorage.setItem('access_token', data.accessToken);
            localStorage.setItem('refresh_token', data.refreshToken);
            localStorage.setItem('user_email', email);
        }

        return data;
    },

    async register(userData: any): Promise<any> {
        return fetchWithAuth(`${API_BASE_URL}/Auth/Register`, {
            method: 'POST',
            body: JSON.stringify(userData)
        });
    },

    async logout(): Promise<void> {
        try {
            await fetchWithAuth(`${API_BASE_URL}/Auth/SignOut`, {
                method: 'PUT'
            });
        } catch (error) {
            console.error('Logout error:', error);
        } finally {
            localStorage.removeItem('access_token');
            localStorage.removeItem('refresh_token');
            localStorage.removeItem('user_email');
        }
    },

    async validateToken(): Promise<boolean> {
        const token = localStorage.getItem('access_token');
        if (!token) return false;

        try {
            const response = await fetch(`${API_BASE_URL}/Auth/Validate?token=${token}`, {
                headers: getAuthHeaders()
            });
            return response.ok;
        } catch {
            return false;
        }
    },

    async getCurrentUser(): Promise<any> {
        return fetchWithAuth(`${API_BASE_URL}/User/me`);
    }
};

export const catalogService = {
    async getAllCategories(): Promise<any[]> {
        return fetchWithAuth(`${API_BASE_URL}/Catalog`);
    },

    async getRootCategories(): Promise<any[]> {
        return fetchWithAuth(`${API_BASE_URL}/Catalog/roots`);
    },

    async getSubCategories(parentId: number): Promise<any[]> {
        return fetchWithAuth(`${API_BASE_URL}/Catalog/parent/${parentId}/subcategories`);
    },

    async getCategoryById(id: number): Promise<any> {
        return fetchWithAuth(`${API_BASE_URL}/Catalog/${id}`);
    },

    async createCategory(categoryData: any): Promise<any> {
        return fetchWithAuth(`${API_BASE_URL}/Catalog`, {
            method: 'POST',
            body: JSON.stringify(categoryData)
        });
    },

    async updateCategory(id: number, categoryData: any): Promise<any> {
        return fetchWithAuth(`${API_BASE_URL}/Catalog/${id}`, {
            method: 'PUT',
            body: JSON.stringify(categoryData)
        });
    },

    async deleteCategory(id: number): Promise<any> {
        return fetchWithAuth(`${API_BASE_URL}/Catalog/${id}`, {
            method: 'DELETE'
        });
    }
};

export const userService = {
    async getAllUsers(from: number = 0, count: number = 50): Promise<any[]> {
        return fetchWithAuth(`${API_BASE_URL}/User/All?from=${from}&count=${count}`);
    },

    async getUserById(id: string): Promise<any> {
        return fetchWithAuth(`${API_BASE_URL}/User/${id}`);
    },

    async createUser(userData: any): Promise<any> {
        return fetchWithAuth(`${API_BASE_URL}/User/Add`, {
            method: 'POST',
            body: JSON.stringify(userData)
        });
    },

    async updateUser(id: string, userData: any): Promise<any> {
        return fetchWithAuth(`${API_BASE_URL}/User/Change/${id}`, {
            method: 'PATCH',
            body: JSON.stringify(userData)
        });
    },

    async deleteUser(id: string): Promise<any> {
        return fetchWithAuth(`${API_BASE_URL}/User/Delete/${id}`, {
            method: 'DELETE'
        });
    }
};

export const newsService = {
    async getAllNews(page: number = 1, pageSize: number = 20): Promise<any> {
        return fetchWithAuth(`${API_BASE_URL}/News?page=${page}&pageSize=${pageSize}`);
    },

    async getNewsById(id: number): Promise<any> {
        return fetchWithAuth(`${API_BASE_URL}/News/${id}`);
    },

    async createNews(newsData: any): Promise<any> {
        return fetchWithAuth(`${API_BASE_URL}/News`, {
            method: 'POST',
            body: JSON.stringify(newsData)
        });
    },

    async updateNews(id: number, newsData: any): Promise<any> {
        return fetchWithAuth(`${API_BASE_URL}/News/${id}`, {
            method: 'PUT',
            body: JSON.stringify(newsData)
        });
    },

    async deleteNews(id: number): Promise<any> {
        return fetchWithAuth(`${API_BASE_URL}/News/${id}`, {
            method: 'DELETE'
        });
    }
};

export const shopItemsService = {
    async getAllShopItems(page: number = 1, pageSize: number = 20, category?: string): Promise<any> {
        const params = new URLSearchParams({
            page: page.toString(),
            pageSize: pageSize.toString()
        });

        if (category) {
            params.append('category', category);
        }

        return fetchWithAuth(`${API_BASE_URL}/Shop-items?${params}`);
    },

    async getShopItemById(id: number): Promise<any> {
        return fetchWithAuth(`${API_BASE_URL}/Shop-items/${id}`);
    },

    async createShopItem(itemData: any): Promise<any> {
        return fetchWithAuth(`${API_BASE_URL}/Shop-items`, {
            method: 'POST',
            body: JSON.stringify(itemData)
        });
    },

    async updateShopItem(id: number, itemData: any): Promise<any> {
        return fetchWithAuth(`${API_BASE_URL}/Shop-items/${id}`, {
            method: 'PUT',
            body: JSON.stringify(itemData)
        });
    },

    async deleteShopItem(id: number): Promise<any> {
        return fetchWithAuth(`${API_BASE_URL}/Shop-items/${id}`, {
            method: 'DELETE'
        });
    }
};

export const orderService = {
    async getAllOrders(): Promise<any[]> {
        return fetchWithAuth(`${API_BASE_URL}/Orders`);
    },

    async getOrderById(id: number): Promise<any> {
        return fetchWithAuth(`${API_BASE_URL}/Orders/${id}`);
    },

    async createOrder(orderData: any): Promise<any> {
        return fetchWithAuth(`${API_BASE_URL}/Orders`, {
            method: 'POST',
            body: JSON.stringify(orderData)
        });
    },

    async updateOrder(id: number, orderData: any): Promise<any> {
        return fetchWithAuth(`${API_BASE_URL}/Orders/${id}`, {
            method: 'PUT',
            body: JSON.stringify(orderData)
        });
    },

    async deleteOrder(id: number): Promise<any> {
        return fetchWithAuth(`${API_BASE_URL}/Orders/${id}`, {
            method: 'DELETE'
        });
    }
};