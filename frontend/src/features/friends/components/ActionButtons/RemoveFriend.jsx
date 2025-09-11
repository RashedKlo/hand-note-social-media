import { UserMinusIcon } from "@heroicons/react/24/outline";

const RemoveFriend = ({ friend }) => {
    const isLoading = false;

    return <button
        onClick={() => { }}
        disabled={isLoading}
        className="flex items-center justify-center gap-2 rounded-lg bg-gray-100 px-3 py-2 text-sm font-medium text-gray-700 transition-all duration-200 hover:bg-red-50 hover:text-red-600 disabled:opacity-50"
    >
        <UserMinusIcon className="h-4 w-4" />
        <span className="hidden sm:inline">Remove</span>
    </button>
}
export default RemoveFriend; 