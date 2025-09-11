const Header = () => {
    return <div className="mb-4 sm:mb-6 lg:mb-8">
        <h1
            className="text-xl sm:text-2xl md:text-3xl font-bold mb-1 sm:mb-2"
            style={{ color: 'var(--color-text-primary)' }}
        >
            Settings
        </h1>
        <p className="text-sm md:text-base" style={{ color: 'var(--color-text-secondary)' }}>
            Manage your account settings and preferences
        </p>
    </div>
}
export default Header;