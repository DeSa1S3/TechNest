export interface User {
    id: number;
    username: string;
    email: string;
    role: 'admin' | 'manager' | 'user';
    createdAt: string;
}

export interface CatalogItem {
    id: number;
    name: string;
    category: string;
    description: string;
    price: number;
    sku: string;
    tags: string[];
}

export interface ShopItem {
    id: number;
    title: string;
    sku: string;
    description: string;
    price: number;
    category: string;
    specifications: string[];
    availability: boolean;
    color: string;
    rating: number;
    imageUrl?: string;
    reviews: number;
}

export interface News {
    id: number;
    title: string;
    content: string;
    author: string;
    createdAt: string;
    imageUrl?: string;
    isPublished: boolean;
}

export interface Order {
    id: number;
    userId: number;
    userName: string;
    userEmail: string;
    items: OrderItem[];
    total: number;
    status: 'pending' | 'processing' | 'completed' | 'cancelled';
    createdAt: string;
    address: string;
}

export interface OrderItem {
    productId: number;
    productName: string;
    quantity: number;
    price: number;
    sku: string;
}

export interface Category {
    id: number;
    name: string;
    productCount: number;
}