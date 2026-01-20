export interface CatalogCategory {
    id: number;
    name: string;
    description?: string;
    img?: string;
    parentCategoryId?: number;
    parentCategoryName?: string;
    subCategories: CatalogCategory[];
    createdAt: string;
    updatedAt?: string;
}

export interface ShopItem {
    id: number;
    name: string;
    description?: string;
    price: number;
    quantity?: number;
    category?: string;
    img?: string;
    specifications?: string[];
    availability?: boolean;
    color?: string;
    rating?: number;
    reviewsCount?: number;
    createdAt?: string;
    updatedAt?: string;
}

export interface CatalogDTO {
    name: string;
    description?: string;
    img?: string;
    parentCategoryId?: number;
}

export interface CatalogUpdateDTO {
    name?: string;
    description?: string;
    img?: string;
    parentCategoryId?: number;
}

export interface ShopItemDTO {
    name: string;
    description?: string;
    price: number;
    quantity?: number;
    category?: string;
    img?: string;
    specifications?: string[];
    availability?: boolean;
    color?: string;
}

export interface ApiResponse<T = any> {
    success: boolean;
    data?: T;
    message?: string;
    errors?: string[];
}

export interface PaginatedResponse<T> {
    items: T[];
    totalCount: number;
    page: number;
    pageSize: number;
    totalPages: number;
}