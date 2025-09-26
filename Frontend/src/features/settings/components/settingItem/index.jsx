const SettingItem = ({ icon: Icon, title, description, children, danger = false }) => (
  <div
    className={`p-4 sm:p-6 rounded-xl border transition-all duration-200 hover:shadow-md ${danger
      ? 'border-red-200 hover:border-red-300'
      : 'border-gray-200 hover:border-indigo-300'
      }`}
    style={{
      backgroundColor: 'var(--color-bg-primary)',
      borderColor: danger ? 'var(--color-border-error)' : 'var(--color-border-primary)',
    }}
    onMouseEnter={(e) => {
      e.target.style.borderColor = danger ? 'var(--color-danger)' : 'var(--color-primary)';
    }}
    onMouseLeave={(e) => {
      e.target.style.borderColor = danger ? 'var(--color-border-error)' : 'var(--color-border-primary)';
    }}
  >
    <div className="flex flex-col sm:flex-row items-start gap-4">
      <div
        className={`p-2 rounded-lg flex-shrink-0`}
        style={{
          backgroundColor: danger ? 'var(--color-danger-light)' : 'var(--color-bg-active)'
        }}
      >
        <Icon
          className="w-5 h-5"
          style={{
            color: danger ? 'var(--color-danger)' : 'var(--color-primary)'
          }}
        />
      </div>
      <div className="flex-1 w-full">
        <h3
          className="font-semibold mb-1 text-sm sm:text-base"
          style={{
            color: danger ? 'var(--color-danger)' : 'var(--color-text-primary)'
          }}
        >
          {title}
        </h3>
        <p className="text-sm mb-4" style={{ color: 'var(--color-text-secondary)' }}>
          {description}
        </p>
        {children}
      </div>
    </div>
  </div>
);

export default SettingItem;