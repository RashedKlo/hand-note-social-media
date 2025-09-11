const FriendsPreview = ({ profileUser, mockFriends }) => {
    return <div className="bg-white rounded-lg shadow-sm p-4">
        <div className="flex items-center justify-between mb-3">
            <h3 className="text-lg font-semibold text-gray-900">Friends</h3>
            <button className="text-blue-600 text-sm hover:underline">See all friends</button>
        </div>
        <p className="text-gray-600 text-sm mb-3">{profileUser.friendsCount.toLocaleString()} friends</p>
        <div className="grid grid-cols-3 gap-2">
            {mockFriends.slice(0, 9).map(friend => (
                <div key={friend.id} className="text-center">
                    <img
                        src={friend.avatar}
                        alt={friend.name}
                        className="w-full aspect-square rounded-lg object-cover mb-1"
                    />
                    <p className="text-xs text-gray-700 truncate">{friend.name}</p>
                </div>
            ))}
        </div>
    </div>
}
export default FriendsPreview;