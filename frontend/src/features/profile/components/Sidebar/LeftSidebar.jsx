import AboutPreview from "../Preview/AboutPreview";
import FriendsPreview from "../Preview/FriendsPreview";
import PhotosPreview from "../Preview/PhotosPreview";

const LeftSidebar = ({ profileUser, mockFriends, mockPhotos }) => {
    return <div className="lg:col-span-1 space-y-4">
        <AboutPreview profileUser={profileUser} />
        <FriendsPreview mockFriends={mockFriends} profileUser={profileUser} />
        <PhotosPreview mockPhotos={mockPhotos} />
    </div>
}
export default LeftSidebar; 