import React, { useState, useEffect } from 'react';
import { userService } from '../../service/apiServices';
import './admin_panel_user.sass';

interface User {
    id: string;
    username: string;
    email: string;
    role: string;
    createdAt: string;
    firstName?: string;
    lastName?: string;
    status?: string;
}

const AdminUsers: React.FC = () => {
    const [users, setUsers] = useState<User[]>([]);
    const [showModal, setShowModal] = useState(false);
    const [editingUser, setEditingUser] = useState<User | null>(null);
    const [formData, setFormData] = useState({
        username: '',
        email: '',
        role: 'user' as string,
        firstName: '',
        lastName: '',
        password: '',
    });
    const [searchTerm, setSearchTerm] = useState('');
    const [roleFilter, setRoleFilter] = useState<string>('all');
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        loadUsers();
    }, []);

    const loadUsers = async () => {
        try {
            setLoading(true);
            const data = await userService.getAllUsers(0, 50);

            const formattedUsers: User[] = data.map((user: any) => ({
                id: user.id,
                username: user.firstName || user.email,
                email: user.email,
                role: user.roles?.[0] || 'user',
                createdAt: user.RegistrationDate || user.created_at,
                firstName: user.firstName,
                lastName: user.lastName,
                status: user.status || 'active'
            }));

            setUsers(formattedUsers);
        } catch (error) {
            console.error('Ошибка загрузки пользователей:', error);
            setUsers([
                { id: '1', username: 'admin', email: 'admin@technest.ru', role: 'admin', createdAt: '2024-01-15' },
                { id: '2', username: 'manager', email: 'manager@technest.ru', role: 'manager', createdAt: '2024-02-10' },
                { id: '3', username: 'ivanov', email: 'ivanov@mail.ru', role: 'user', createdAt: '2024-03-05' },
            ]);
        } finally {
            setLoading(false);
        }
    };

    const handleAddUser = () => {
        setEditingUser(null);
        setFormData({
            username: '',
            email: '',
            role: 'user',
            firstName: '',
            lastName: '',
            password: '',
        });
        setShowModal(true);
    };

    const handleEditUser = (user: User) => {
        setEditingUser(user);
        setFormData({
            username: user.username,
            email: user.email,
            role: user.role,
            firstName: user.firstName || '',
            lastName: user.lastName || '',
            password: '',
        });
        setShowModal(true);
    };

    const handleDeleteUser = async (id: string) => {
        if (window.confirm('Вы уверены, что хотите удалить этого пользователя?')) {
            try {
                await userService.deleteUser(id);
                await loadUsers();
                alert('Пользователь успешно удален');
            } catch (error: any) {
                alert(`Ошибка удаления: ${error.message}`);
            }
        }
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        if (!formData.username.trim() || !formData.email.trim()) {
            alert('Пожалуйста, заполните все обязательные поля');
            return;
        }

        try {
            const userData = {
                firstName: formData.firstName || formData.username,
                lastName: formData.lastName || '',
                Email: formData.email,
                Password: formData.password || 'defaultPassword123',
                Roles: [formData.role],
                status: 'active'
            };

            if (editingUser) {
                await userService.updateUser(editingUser.id, userData);
            } else {
                await userService.createUser(userData);
            }

            await loadUsers();
            setShowModal(false);
            alert(editingUser ? 'Пользователь успешно обновлен' : 'Пользователь успешно создан');

        } catch (err: any) {
            alert(`Ошибка сохранения: ${err.message}`);
        }
    };

    const filteredUsers = users.filter(user => {
        const matchesSearch = user.username.toLowerCase().includes(searchTerm.toLowerCase()) ||
            user.email.toLowerCase().includes(searchTerm.toLowerCase());
        const matchesRole = roleFilter === 'all' || user.role === roleFilter;
        return matchesSearch && matchesRole;
    });

    const getRoleName = (role: string) => {
        switch (role) {
            case 'admin': return 'Администратор';
            case 'manager': return 'Менеджер';
            case 'user': return 'Пользователь';
            default: return role;
        }
    };

    if (loading) {
        return (
            <div className="users-container">
                <div className="loading">Загрузка пользователей...</div>
            </div>
        );
    }
    return (
        <div className="users-container">
            <div className="users-header">
                <div>
                    <h2>Управление пользователями</h2>
                    <p className="section-subtitle">Всего пользователей: {users.length}</p>
                </div>
                <button className="btn-primary" onClick={handleAddUser}>
                    <span>+</span> Добавить пользователя
                </button>
            </div>

            <div className="users-filters">
                <div className="filter-group">
                    <label htmlFor="search">Поиск:</label>
                    <input
                        type="text"
                        id="search"
                        placeholder="Введите имя или email..."
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
                        style={{ width: '250px' }}
                    />
                </div>
                <div className="filter-group">
                    <label htmlFor="role-filter">Роль:</label>
                    <select
                        id="role-filter"
                        value={roleFilter}
                        onChange={(e) => setRoleFilter(e.target.value)}
                    >
                        <option value="all">Все роли</option>
                        <option value="admin">Администраторы</option>
                        <option value="editor">Редакторы</option>
                        <option value="user">Пользователи</option>
                    </select>
                </div>
            </div>

            <div className="users-table-container">
                <table className="users-table">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Имя пользователя</th>
                            <th>Email</th>
                            <th>Роль</th>
                            <th>Дата регистрации</th>
                            <th>Статус</th>
                            <th>Действия</th>
                        </tr>
                    </thead>
                    <tbody>
                        {filteredUsers.map(user => (
                            <tr key={user.id}>
                                <td>#{user.id.toString().padStart(4, '0')}</td>
                                <td>
                                    <div style={{ display: 'flex', alignItems: 'center', gap: '10px' }}>
                                        <div style={{
                                            width: '32px',
                                            height: '32px',
                                            borderRadius: '50%',
                                            background: 'linear-gradient(135deg, #0071e3, #34c759)',
                                            color: 'white',
                                            display: 'flex',
                                            alignItems: 'center',
                                            justifyContent: 'center',
                                            fontWeight: '600'
                                        }}>
                                            {user.username.charAt(0).toUpperCase()}
                                        </div>
                                        {user.username}
                                    </div>
                                </td>
                                <td className="user-email">{user.email}</td>
                                <td>
                                    <span className={`user-role role-${user.role}`}>
                                        {getRoleName(user.role)}
                                    </span>
                                </td>
                                <td>{user.createdAt}</td>
                                <td>
                                    <div className="user-status">
                                        <div className="status-dot active"></div>
                                        <span>Активен</span>
                                    </div>
                                </td>
                                <td>
                                    <div className="user-actions">
                                        <button
                                            className="btn-secondary"
                                            onClick={() => handleEditUser(user)}
                                            style={{ padding: '6px 12px', fontSize: '13px' }}
                                        >
                                            ✏️ Изменить
                                        </button>
                                        {user.role !== 'admin' && (
                                            <button
                                                className="btn-danger"
                                                onClick={() => handleDeleteUser(user.id)}
                                                style={{ padding: '6px 12px', fontSize: '13px' }}
                                            >
                                                🗑️ Удалить
                                            </button>
                                        )}
                                    </div>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>

            {showModal && (
                <div className="modal-overlay" onClick={() => setShowModal(false)}>
                    <div className="modal" onClick={(e) => e.stopPropagation()}>
                        <div className="modal-header">
                            <h2>{editingUser ? 'Изменить пользователя' : 'Добавить нового пользователя'}</h2>
                            <button className="close-btn" onClick={() => setShowModal(false)}>×</button>
                        </div>
                        <form onSubmit={handleSubmit}>
                            <div className="modal-body">
                                <div className="form-group">
                                    <label htmlFor="username">
                                        Имя пользователя
                                        <span className="required">*</span>
                                    </label>
                                    <input
                                        type="text"
                                        id="username"
                                        value={formData.username}
                                        onChange={(e) => setFormData({ ...formData, username: e.target.value })}
                                        placeholder="Введите имя пользователя"
                                        required
                                    />
                                </div>
                                <div className="form-group">
                                    <label htmlFor="email">
                                        Email
                                        <span className="required">*</span>
                                    </label>
                                    <input
                                        type="email"
                                        id="email"
                                        value={formData.email}
                                        onChange={(e) => setFormData({ ...formData, email: e.target.value })}
                                        placeholder="example@technest.ru"
                                        required
                                    />
                                </div>
                                <div className="form-group">
                                    <label htmlFor="role">Роль</label>
                                    <select
                                        id="role"
                                        value={formData.role}
                                        onChange={(e) => setFormData({ ...formData, role: e.target.value as User['role'] })}
                                    >
                                        <option value="user">Пользователь</option>
                                        <option value="editor">Редактор</option>
                                        <option value="admin">Администратор</option>
                                    </select>
                                </div>
                            </div>
                            <div className="modal-footer">
                                <button
                                    type="button"
                                    className="btn-secondary"
                                    onClick={() => setShowModal(false)}
                                >
                                    Отмена
                                </button>
                                <button type="submit" className="btn-primary">
                                    {editingUser ? 'Сохранить изменения' : 'Создать пользователя'}
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            )}
        </div>
    );
};

export default AdminUsers;