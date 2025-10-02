import { XMarkIcon } from "@heroicons/react/24/outline";
import { useDeclineFriendRequest } from "../../hooks/useDeclineFriendRequest";

const RejectRequest = ({ request }) => {
    const { declineFriendRequest, isLoading } = useDeclineFriendRequest();
    return <button
        onClick={() => declineFriendRequest(request?.friendshipId, request?.userId)}
        disabled={isLoading}
        className="flex items-center justify-center gap-2 rounded-lg bg-gray-100 px-3 py-2 text-sm font-medium text-gray-700 transition-all duration-200 hover:bg-gray-200 disabled:opacity-50"
    >
        <XMarkIcon className="h-4 w-4" />
        <span className="hidden sm:inline">Decline</span>
    </button>
}
export default RejectRequest;
