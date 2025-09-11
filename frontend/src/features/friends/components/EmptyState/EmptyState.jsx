
const EmptyState = ({ title, description, Icon }) => {
    return (
        <div className="py-16 text-center">
            <div className="mx-auto mb-6 flex h-20 w-20 items-center justify-center rounded-2xl bg-blue-100">
                {Icon}
            </div>
            <h3 className="mb-3 text-xl font-bold text-gray-900">{title}</h3>
            <p className="mx-auto mb-6 max-w-md text-gray-500">{description}</p>
        </div>
    );
};
export default EmptyState;