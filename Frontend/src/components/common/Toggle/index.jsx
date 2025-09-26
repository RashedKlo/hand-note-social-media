// Toggle Component
const Toggle = ({ enabled, onChange, label }) => (
  <div className="flex items-center justify-between gap-3">
    <span className="text-sm font-medium flex-1" style={{ color: 'var(--color-text-primary)' }}>
      {label}
    </span>
    <button
      onClick={() => onChange(!enabled)}
      className="relative w-11 h-6 rounded-full transition-colors duration-200 flex-shrink-0"
      style={{
        backgroundColor: enabled ? 'var(--color-primary)' : 'var(--color-border-secondary)'
      }}
    >
      <span
        className={`absolute top-0.5 left-0.5 w-5 h-5 bg-white rounded-full transition-transform duration-200 ${enabled ? 'translate-x-5' : 'translate-x-0'
          }`}
      />
    </button>
  </div>
);
export default Toggle;