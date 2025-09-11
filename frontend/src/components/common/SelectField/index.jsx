// SelectField Component
const SelectField = ({ value, onChange, options, label }) => (
  <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-2 sm:gap-3">
    <span className="text-sm font-medium flex-1" style={{ color: 'var(--color-text-primary)' }}>
      {label}
    </span>
    <select
      value={value}
      onChange={(e) => onChange(e.target.value)}
      className="px-3 py-2 border rounded-lg text-sm focus:outline-none focus:ring-2 w-full sm:w-auto min-w-0 sm:min-w-[160px] transition-all duration-200"
      style={{
        backgroundColor: 'var(--color-bg-primary)',
        borderColor: 'var(--color-border-secondary)',
        color: 'var(--color-text-primary)'
      }}
      onFocus={(e) => {
        e.target.style.borderColor = 'var(--color-border-focus)';
        e.target.style.boxShadow = `0 0 0 2px rgba(24, 119, 242, 0.2)`;
      }}
      onBlur={(e) => {
        e.target.style.borderColor = 'var(--color-border-secondary)';
        e.target.style.boxShadow = 'none';
      }}
    >
      {options.map(option => (
        <option key={option.value} value={option.value}>{option.label}</option>
      ))}
    </select>
  </div>
);

export default SelectField;