import { CheckIcon } from "@heroicons/react/24/outline";

const AcceptRequest = ({ request }) => {
    const isLoading = false;

    return <button
        onClick={() => { }}
        disabled={isLoading}
        className="btn-secondary-custom flex flex-1 items-center justify-center gap-2 text-sm disabled:opacity-50"
    >
        <CheckIcon className="h-4 w-4" />
        <span className="hidden sm:inline">Accept</span>
    </button>
}
export default AcceptRequest; 