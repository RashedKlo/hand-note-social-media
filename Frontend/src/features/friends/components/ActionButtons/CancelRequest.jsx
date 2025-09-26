import { ClockIcon } from "@heroicons/react/24/outline";

const CancelRequest = ({ request }) => {
    const isLoading = false;

    return <button
        onClick={() => { }}
        disabled={isLoading}
        className="btn-primary flex w-full items-center justify-center gap-2 bg-amber-500 text-sm font-medium text-white transition-all duration-200 hover:bg-amber-600 disabled:opacity-50"
    >
        <ClockIcon className="h-4 w-4" />
        <span className="hidden sm:inline">Cancel Request</span>
    </button>
};
export default CancelRequest;