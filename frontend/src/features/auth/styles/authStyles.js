// authStyles.js - Centralized styles for authentication forms

export const authStyles = {
  // Base input styling
  inputBase: "w-full pl-4 pr-12 py-3.5 sm:py-4 text-base sm:text-lg rounded-2xl bg-white/60 backdrop-blur-sm border border-slate-200/50 transition-all duration-300 hover:bg-white/80 hover:border-slate-300/70 hover:shadow-md focus:bg-white focus:border-2 focus:border-indigo-500 focus:shadow-lg focus:outline-none placeholder-slate-400",
  
  // State-specific input styling
  inputError: "border-red-400 focus:border-red-500 bg-red-50/30",
  inputSuccess: "border-green-400 bg-green-50/30",
  inputFocused: "ring-4 ring-indigo-100",
  
  // Label styling
  label: "block text-slate-700 font-semibold mb-2 text-sm sm:text-base tracking-wide",
  
  // Message styling
  errorText: "text-red-500 text-sm mt-2 font-medium flex items-center space-x-1",
  successText: "text-green-500 text-sm mt-2 font-medium flex items-center space-x-1",
  
  // Icon styling
  icon: {
    base: "w-5 h-5 absolute right-3 top-1/2 transform -translate-y-1/2 transition-colors duration-200",
    error: "text-red-500",
    success: "text-green-500",
    default: "text-slate-400"
  },
  
  // Button styling for password toggle
  passwordToggle: "absolute right-3 top-1/2 transform -translate-y-1/2 text-slate-400 hover:text-slate-600 transition-colors duration-200 focus:outline-none"
};

// Helper function to combine input classes based on field status and focus
export const getInputClasses = (fieldStatus, isFocused) => {
  let classes = authStyles.inputBase;
  
  if (fieldStatus === 'error') {
    classes += ` ${authStyles.inputError}`;
  } else if (fieldStatus === 'success') {
    classes += ` ${authStyles.inputSuccess}`;
  }
  
  if (isFocused) {
    classes += ` ${authStyles.inputFocused}`;
  }
  
  return classes;
};

// Helper function to get icon classes based on status
export const getIconClasses = (status) => {
  const baseClasses = authStyles.icon.base;
  
  switch (status) {
    case 'error':
      return `${baseClasses} ${authStyles.icon.error}`;
    case 'success':
      return `${baseClasses} ${authStyles.icon.success}`;
    default:
      return `${baseClasses} ${authStyles.icon.default}`;
  }
};