export default function SocialLogin() {
    return (
        <div className="mt-8 pt-6 border-t border-slate-200">
            <p className="text-center text-slate-500 mb-6 text-sm font-medium tracking-wide">
                Or sign in with
            </p>
            <div className="flex flex-col sm:flex-row gap-3">
                <button
                    type="button"
                    className="flex-1 py-3 px-4 bg-gradient-to-r from-white to-slate-50 border border-slate-300 rounded-full hover:scale-[1.02] hover:brightness-105 transition-all duration-200 flex items-center justify-center gap-3 font-medium text-slate-700 shadow-sm"
                >
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 48 48" className="w-5 h-5">
                        <path fill="#4285F4" d="M24 9.5c3.54 0 6.72 1.23 9.23 3.64l6.9-6.9C35.99 2.4 30.41 0 24 0 14.62 0 6.4 5.54 2.54 13.46l8.08 6.27C12.85 13.39 17.95 9.5 24 9.5z"/>
                        <path fill="#34A853" d="M46.1 24.55c0-1.57-.14-3.08-.4-4.55H24v9.09h12.37c-.54 2.88-2.15 5.31-4.58 6.95l7.08 5.49C43.74 37.1 46.1 31.2 46.1 24.55z"/>
                        <path fill="#FBBC05" d="M10.62 28.73a14.46 14.46 0 0 1-.75-4.23c0-1.47.27-2.88.75-4.23l-8.08-6.27A23.94 23.94 0 0 0 0 24.5c0 3.92.94 7.63 2.54 10.9l8.08-6.27z"/>
                        <path fill="#EA4335" d="M24 48c6.48 0 11.91-2.13 15.87-5.8l-7.08-5.49c-2.04 1.38-4.63 2.19-7.79 2.19-6.05 0-11.15-3.89-13.38-9.23l-8.08 6.27C6.4 42.46 14.62 48 24 48z"/>
                    </svg>
                    <span>Google</span>
                </button>
                <button
                    type="button"
                    className="flex-1 py-3 px-4 bg-gradient-to-r from-white to-slate-50 border border-slate-300 rounded-full hover:scale-[1.02] hover:brightness-105 transition-all duration-200 flex items-center justify-center gap-3 font-medium text-slate-700 shadow-sm"
                >
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 48 48" className="w-5 h-5">
                        <path fill="#1877F2" d="M24 0C10.74 0 0 10.74 0 24c0 11.96 8.79 21.87 20.25 23.74v-16.8h-6.1v-6.94h6.1v-5.3c0-6.02 3.58-9.34 9.07-9.34 2.63 0 5.39.47 5.39.47v5.92h-3.04c-3 0-3.93 1.87-3.93 3.78v4.47h6.7l-1.07 6.94h-5.63v16.8C39.21 45.87 48 35.96 48 24 48 10.74 37.26 0 24 0z"/>
                    </svg>
                    <span>Facebook</span>
                </button>
            </div>
        </div>
    );
}
