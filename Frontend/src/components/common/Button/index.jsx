 const Button = ({ variant = 'primary', children, onClick, className = '', size = 'default' }) => {
    const sizes = {
      small: 'px-3 py-1.5 text-xs',
      default: 'px-4 py-2 text-sm',
      large: 'px-6 py-3 text-base'
    };

    const getButtonStyle = (variant) => {
      switch (variant) {
        case 'primary':
          return {
            background: `linear-gradient(135deg, var(--primary-from) 0%, var(--primary-to) 100%)`,
            color: 'white',
            border: 'none'
          };
        case 'secondary':
          return {
            backgroundColor: 'var(--bg-tertiary)',
            color: 'var(--text-primary)',
            border: `1px solid var(--border-primary)`
          };
        case 'danger':
          return {
            backgroundColor: 'var(--danger)',
            color: 'white',
            border: 'none'
          };
        default:
          return {};
      }
    };

    return (
      <button
        onClick={onClick}
        className={`${sizes[size]} rounded-lg font-medium transition-all duration-200 hover:shadow-md hover:-translate-y-0.5 ${className}`}
        style={getButtonStyle(variant)}
        onMouseEnter={(e) => {
          if (variant === 'secondary') {
            e.target.style.backgroundColor = 'var(--primary-lighter)';
            e.target.style.borderColor = 'var(--primary-from)';
          }
        }}
        onMouseLeave={(e) => {
          if (variant === 'secondary') {
            e.target.style.backgroundColor = 'var(--bg-tertiary)';
            e.target.style.borderColor = 'var(--border-primary)';
          }
        }}
      >
        {children}
      </button>
    );
  };
  export default Button;