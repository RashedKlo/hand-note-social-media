import { CheckIcon } from "@heroicons/react/24/outline";
import { useAcceptFriendRequest } from "../../hooks/useAcceptFriendRequest";

const AcceptRequest = ({ request }) => {
    const { isLoading, acceptFriendRequest } = useAcceptFriendRequest();

    return <button
        onClick={() => () => acceptFriendRequest(request?.friendshipId, request?.userId)}
        disabled={isLoading}
        className="btn-secondary-custom flex flex-1 items-center justify-center gap-2 text-sm disabled:opacity-50"
    >
        <CheckIcon className="h-4 w-4" />
        <span className="hidden sm:inline">Accept</span>
    </button>
}
export default AcceptRequest; 