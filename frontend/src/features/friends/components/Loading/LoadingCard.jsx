const LoadingCard = () => (
    <div className="card animate-pulse p-4 sm:p-5">
        <div className="flex items-start gap-3 sm:gap-4">
            <div className="w-12 h-12 sm:w-14 sm:h-14 rounded-lg bg-gray-200" />
            <div className="flex-1">
                <div className="mb-2 h-4 w-3/4 rounded bg-gray-200" />
                <div className="mb-3 h-3 w-1/2 rounded bg-gray-200" />
                <div className="flex gap-2">
                    <div className="h-8 flex-1 rounded bg-gray-200" />
                    <div className="h-8 w-20 rounded bg-gray-200" />
                </div>
            </div>
        </div>
    </div>
);
export default LoadingCard;