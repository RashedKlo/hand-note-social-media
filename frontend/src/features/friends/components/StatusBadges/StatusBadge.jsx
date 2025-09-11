// Configuration for user status badges.
const FRIEND_STATUSES = {
    online: { color: 'bg-green-500', label: 'Online' },
    away: { color: 'bg-yellow-500', label: 'Away' },
    offline: { color: 'bg-gray-400', label: 'Offline' },
    busy: { color: 'bg-red-500', label: 'Busy' },
};
const StatusBadge = ({ status, isOnline }) => {
    if (!status || !isOnline) return null;

    const statusColor = FRIEND_STATUSES[status]?.color || 'bg-gray-400';

    return (
        <div
            className={`absolute -bottom-1 -right-1 w-4 h-4 rounded-full border-2 border-white ${statusColor}`}
        >
            <div className="w-full h-full rounded-full animate-pulse" />
        </div>
    );
};
export default StatusBadge;