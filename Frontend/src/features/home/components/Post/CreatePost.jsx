import { PhotoIcon, VideoCameraIcon } from "@heroicons/react/24/outline";
import { FaceSmileIcon } from "@heroicons/react/24/solid";
import { memo } from "react";

// CreatePost Component
const CreatePost = memo(({ currentUser, onOpenModal }) => (
  <div className="bg-white rounded-xl shadow-sm p-2 sm:p-4">
    <div className="flex space-x-2 sm:space-x-3">
      <img src={currentUser.avatar} alt={currentUser.name} className="w-8 h-8 sm:w-10 sm:h-10 rounded-full object-cover" />
      <button onClick={onOpenModal} className="flex-1 bg-gray-100 hover:bg-gray-200 rounded-full px-3 sm:px-4 py-2 sm:py-3 text-left text-gray-500 transition-colors text-xs sm:text-sm">
        What's on your mind, {currentUser.name.split(' ')[0]}?
      </button>
    </div>
    <div className="flex items-center justify-around mt-2 sm:mt-4 pt-2 sm:pt-4 border-t border-gray-200">
      <button onClick={() => alert('Live video coming soon')} className="flex items-center space-x-1 sm:space-x-2 px-2 sm:px-4 py-1 sm:py-2 hover:bg-gray-100 rounded-lg transition-colors">
        <VideoCameraIcon className="w-5 h-5 sm:w-6 sm:h-6 text-red-500" />
        <span className="text-gray-600 font-medium text-xs sm:text-base">Live video</span>
      </button>
      <button onClick={onOpenModal} className="flex items-center space-x-1 sm:space-x-2 px-2 sm:px-4 py-1 sm:py-2 hover:bg-gray-100 rounded-lg transition-colors">
        <PhotoIcon className="w-5 h-5 sm:w-6 sm:h-6 text-green-500" />
        <span className="text-gray-600 font-medium text-xs sm:text-base">Photo/video</span>
      </button>
      <button onClick={onOpenModal} className="flex items-center space-x-1 sm:space-x-2 px-2 sm:px-4 py-1 sm:py-2 hover:bg-gray-100 rounded-lg transition-colors">
        <FaceSmileIcon className="w-5 h-5 sm:w-6 sm:h-6 text-yellow-500" />
        <span className="text-gray-600 font-medium text-xs sm:text-base">Feeling/activity</span>
      </button>
    </div>
  </div>
));
export default CreatePost;