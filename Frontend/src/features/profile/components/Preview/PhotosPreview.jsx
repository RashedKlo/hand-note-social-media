const PhotosPreview = ({ mockPhotos }) => {
    {/* Photos Preview */ }
    return <div className="bg-white rounded-lg shadow-sm p-4">
        <div className="flex items-center justify-between mb-3">
            <h3 className="text-lg font-semibold text-gray-900">Photos</h3>
            <button className="text-blue-600 text-sm hover:underline">See all photos</button>
        </div>
        <div className="grid grid-cols-3 gap-2">
            {mockPhotos.slice(0, 9).map((photo, index) => (
                <img
                    key={index}
                    src={photo}
                    alt={`Photo ${index + 1}`}
                    className="w-full aspect-square rounded-lg object-cover"
                />
            ))}
        </div>
    </div>
}
export default PhotosPreview;