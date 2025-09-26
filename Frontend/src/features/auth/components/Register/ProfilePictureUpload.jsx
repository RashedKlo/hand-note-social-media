export default function ProfilePictureUpload({ profilePicture, onImageChange, uploadRef }) {
    const handleClick = () => {
        uploadRef.current?.click();
    };

    return (
        <div className="text-center mb-4">
            <div className="relative inline-block">
                <div
                    onClick={handleClick}
                    className="w-20 h-20 sm:w-24 sm:h-24 rounded-full border-4 border-indigo-500 cursor-pointer transition-all duration-300 shadow-lg hover:shadow-xl hover:scale-105 bg-gray-200 flex items-center justify-center overflow-hidden"
                    style={{
                        boxShadow: '0 8px 25px rgba(102, 126, 234, 0.3)',
                    }}
                >
                    {profilePicture ? (
                        <img src={profilePicture} alt="Profile" className="w-full h-full object-cover" />
                    ) : (
                        <span className="text-gray-500 text-sm">📷</span>
                    )}
                </div>
                <button
                    type="button"
                    onClick={handleClick}
                    className="absolute -bottom-1 -right-1 w-9 h-9 bg-gradient-to-br from-indigo-500 to-purple-600 text-white rounded-full shadow-lg hover:shadow-xl transform hover:scale-110 transition-all duration-300 flex items-center justify-center focus:outline-none focus:ring-2 focus:ring-indigo-300"
                >
                    <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M11 5H6a2 2 0 00-2 2v11a2 2 0 002 2h11a2 2 0 002-2v-5m-1.414-9.414a2 2 0 112.828 2.828L11.828 15H9v-2.828l8.586-8.586z" />
                    </svg>
                </button>  
            </div>
            <p className="text-slate-500 mt-3 text-sm font-medium">
                Click to add profile picture
            </p>
        </div>
    );
}