import { CameraIcon, ChatBubbleLeftIcon, EllipsisHorizontalIcon, UserPlusIcon } from "@heroicons/react/24/outline";
import { useState } from "react";

const ProfileHeader = ({ profileUser, mockFriends, posts, activeTab, setActiveTab }) => {
    const [isFollowing, setIsFollowing] = useState(false);

    return <div className="bg-white shadow-sm">
        {/* Cover Photo */}
        <div className="relative h-64 sm:h-80 lg:h-96">
            <img
                src={profileUser.coverPhoto}
                alt="Cover"
                className="w-full h-full object-cover"
            />
            <div className="absolute inset-0 bg-black bg-opacity-20"></div>
            <button className="absolute bottom-4 right-4 bg-white hover:bg-gray-100 text-gray-800 px-4 py-2 rounded-lg shadow-md flex items-center space-x-2 transition-colors">
                <CameraIcon className="w-4 h-4" />
                <span className="text-sm font-medium hidden sm:inline">Edit cover photo</span>
            </button>
        </div>

        {/* Profile Info Section */}
        <div className="relative px-4 sm:px-6 lg:px-8">
            <div className="flex flex-col sm:flex-row sm:items-end sm:space-x-6 pb-4">
                {/* Profile Picture */}
                <div className="relative -mt-16 sm:-mt-20">
                    <div className="relative">
                        <img
                            src={profileUser.avatar}
                            alt={profileUser.name}
                            className="w-32 h-32 sm:w-40 sm:h-40 rounded-full border-4 border-white object-cover shadow-lg bg-white"
                        />
                        <button className="absolute bottom-2 right-2 bg-gray-200 hover:bg-gray-300 p-2 rounded-full shadow-md transition-colors">
                            <CameraIcon className="w-5 h-5 text-gray-700" />
                        </button>
                    </div>
                </div>

                {/* Name and Stats */}
                <div className="flex-1 mt-4 sm:mt-0">
                    <div className="flex flex-col sm:flex-row sm:items-end sm:justify-between">
                        <div>
                            <h1 className="text-2xl sm:text-3xl font-bold text-gray-900">{profileUser.name}</h1>
                            <p className="text-gray-600 mt-1">{profileUser.friendsCount.toLocaleString()} friends</p>
                            <div className="flex -space-x-1 mt-2">
                                {mockFriends.slice(0, 8).map(friend => (
                                    <img
                                        key={friend.id}
                                        src={friend.avatar}
                                        alt={friend.name}
                                        className="w-8 h-8 rounded-full border-2 border-white object-cover"
                                    />
                                ))}
                            </div>
                        </div>

                        {/* Action Buttons */}
                        <div className="flex items-center space-x-2 mt-4 sm:mt-0">
                            <button
                                onClick={() => setIsFollowing(!isFollowing)}
                                className={`px-6 py-2 rounded-lg font-medium transition-colors ${isFollowing
                                    ? 'bg-gray-200 hover:bg-gray-300 text-gray-800'
                                    : 'bg-blue-600 hover:bg-blue-700 text-white'
                                    }`}
                            >
                                {isFollowing ? (
                                    <>
                                        <UserPlusIcon className="w-4 h-4 inline mr-2" />
                                        Following
                                    </>
                                ) : (
                                    <>
                                        <UserPlusIcon className="w-4 h-4 inline mr-2" />
                                        Follow
                                    </>
                                )}
                            </button>
                            <button className="bg-gray-200 hover:bg-gray-300 text-gray-800 px-6 py-2 rounded-lg font-medium transition-colors">
                                <ChatBubbleLeftIcon className="w-4 h-4 inline mr-2" />
                                Message
                            </button>
                            <button className="bg-gray-200 hover:bg-gray-300 text-gray-800 p-2 rounded-lg transition-colors">
                                <EllipsisHorizontalIcon className="w-5 h-5" />
                            </button>
                        </div>
                    </div>
                </div>
            </div>

            {/* Bio */}
            <div className="border-t border-gray-200 pt-4">
                <p className="text-gray-700 text-sm sm:text-base max-w-2xl">{profileUser.bio}</p>
            </div>

            {/* Navigation Tabs */}
            <div className="border-t border-gray-200 mt-4">
                <nav className="flex space-x-8 overflow-x-auto">
                    {[
                        { id: 'posts', name: 'Posts', count: posts.length },
                        { id: 'about', name: 'About' },
                        { id: 'friends', name: 'Friends', count: profileUser.friendsCount },
                        { id: 'photos', name: 'Photos', count: profileUser.photosCount },
                        { id: 'videos', name: 'Videos' },
                        { id: 'reviews', name: 'Reviews' },
                    ].map((tab) => (
                        <button
                            key={tab.id}
                            className={`py-4 px-1 text-sm font-medium border-b-2 whitespace-nowrap transition-colors ${activeTab === tab.id
                                ? 'text-blue-600 border-blue-600'
                                : 'text-gray-500 border-transparent hover:text-gray-700 hover:border-gray-300'
                                }`}
                            onClick={() => setActiveTab(tab.id)}
                        >
                            {tab.name}
                            {tab.count && (
                                <span className="ml-2 text-xs bg-gray-200 text-gray-600 px-2 py-0.5 rounded-full">
                                    {tab.count > 999 ? `${Math.floor(tab.count / 1000)}k` : tab.count}
                                </span>
                            )}
                        </button>
                    ))}
                </nav>
            </div>
        </div>
    </div>
}
export default ProfileHeader;