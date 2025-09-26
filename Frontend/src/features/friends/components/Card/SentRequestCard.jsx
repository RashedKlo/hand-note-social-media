import { EllipsisHorizontalIcon, UsersIcon } from "@heroicons/react/24/outline";
import StatusBadge from "../StatusBadges/StatusBadge";
import CancelRequest from "../ActionButtons/CancelRequest";

const SentRequestCard = ({ request }) => {
    return (
        <div className={`card p-4 sm:p-5 transition-all duration-300 `}>
            < div className="flex items-start gap-3 sm:gap-4">
                <div className="group relative flex-shrink-0">
                    <img
                        src={request.avatar}
                        alt={request.name}
                        className="h-12 w-12 rounded-lg object-cover transition-transform duration-200 group-hover:scale-105 sm:h-14 sm:w-14"
                    // Fallback for broken image URLs

                    />
                    <StatusBadge status={request.status} isOnline={request.isOnline} />
                </div>

                <div className="min-w-0 flex-1">
                    <div className="mb-3 flex items-start justify-between gap-2">
                        <div className="min-w-0 flex-1">
                            <h3 className="truncate text-sm font-semibold text-gray-900 sm:text-base">
                                {request.name}
                            </h3>
                            <div className="flex items-center gap-1 text-xs text-gray-500 sm:text-sm">
                                <UsersIcon className="h-3 w-3 flex-shrink-0" />
                                <span>{request.mutualFriends} mutual friends</span>
                            </div>
                            {/* Display dynamic status info based on type */}
                            {request.sentDate && (
                                <p className="mt-1 text-xs text-gray-400">Sent: {request.sentDate}</p>
                            )}
                        </div>

                        <div className="flex items-center gap-1">
                            {request.isOnline && (
                                <div className="h-2 w-2 animate-pulse rounded-full bg-green-500" />
                            )}
                            <button className="rounded-lg p-1.5 transition-colors hover:bg-gray-100">
                                <EllipsisHorizontalIcon className="h-4 w-4 text-gray-400" />
                            </button>
                        </div>
                    </div>

                    <CancelRequest request={request} />

                </div>
            </div>
        </div>
    );
};
export default SentRequestCard;

