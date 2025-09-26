import { EllipsisHorizontalIcon, UsersIcon } from "@heroicons/react/24/outline";
import StatusBadge from "../StatusBadges/StatusBadge";
import MessageFriend from "../ActionButtons/MessageFriend";
import RemoveFriend from "../ActionButtons/RemoveFriend";


const FriendCard = ({ friend }) => {
    return (
        <div className={`card p-4 sm:p-5 transition-all duration-300 `}>
            < div className="flex items-start gap-3 sm:gap-4">
                <div className="group relative flex-shrink-0">
                    <img
                        src={friend.avatar}
                        alt={friend.name}
                        className="h-12 w-12 rounded-lg object-cover transition-transform duration-200 group-hover:scale-105 sm:h-14 sm:w-14"
                    />
                    <StatusBadge status={friend.status} isOnline={friend.isOnline} />
                </div>

                <div className="min-w-0 flex-1">
                    <div className="mb-3 flex items-start justify-between gap-2">
                        <div className="min-w-0 flex-1">
                            <h3 className="truncate text-sm font-semibold text-gray-900 sm:text-base">
                                {friend.name}
                            </h3>
                            <div className="flex items-center gap-1 text-xs text-gray-500 sm:text-sm">
                                <UsersIcon className="h-3 w-3 flex-shrink-0" />
                                <span>{friend.mutualFriends} mutual friends</span>
                            </div>
                            {/* Display dynamic status info based on type */}
                            {friend.lastActive && (
                                <p className="mt-1 text-xs text-gray-400">Last active: {friend.lastActive}</p>
                            )}
                        </div>

                        <div className="flex items-center gap-1">
                            {friend.isOnline && (
                                <div className="h-2 w-2 animate-pulse rounded-full bg-green-500" />
                            )}
                            <button className="rounded-lg p-1.5 transition-colors hover:bg-gray-100">
                                <EllipsisHorizontalIcon className="h-4 w-4 text-gray-400" />
                            </button>
                        </div>
                    </div>
                    <div className="flex gap-2">
                        <MessageFriend friend={friend} />
                        <RemoveFriend friend={friend} />
                    </div>
                </div>
            </div>
        </div>
    );
};
export default FriendCard;

