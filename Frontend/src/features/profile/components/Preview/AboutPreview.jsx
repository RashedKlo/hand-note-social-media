import { AcademicCapIcon, BriefcaseIcon, CalendarIcon, GlobeAltIcon, HeartIcon, MapPinIcon } from "@heroicons/react/24/outline";

const AboutPreview = ({ profileUser }) => {

    return <div className="bg-white rounded-lg shadow-sm p-4">
        <h3 className="text-lg font-semibold text-gray-900 mb-3">About</h3>
        <div className="space-y-3">
            <div className="flex items-center text-gray-600">
                <BriefcaseIcon className="w-5 h-5 mr-3 text-gray-400" />
                <span className="text-sm">Works at <strong>{profileUser.work}</strong></span>
            </div>
            <div className="flex items-center text-gray-600">
                <AcademicCapIcon className="w-5 h-5 mr-3 text-gray-400" />
                <span className="text-sm">Studied at <strong>{profileUser.education}</strong></span>
            </div>
            <div className="flex items-center text-gray-600">
                <MapPinIcon className="w-5 h-5 mr-3 text-gray-400" />
                <span className="text-sm">Lives in <strong>{profileUser.location}</strong></span>
            </div>
            <div className="flex items-center text-gray-600">
                <HeartIcon className="w-5 h-5 mr-3 text-gray-400" />
                <span className="text-sm">{profileUser.relationship}</span>
            </div>
            <div className="flex items-center text-gray-600">
                <GlobeAltIcon className="w-5 h-5 mr-3 text-gray-400" />
                <span className="text-sm">{profileUser.website}</span>
            </div>
            <div className="flex items-center text-gray-600">
                <CalendarIcon className="w-5 h-5 mr-3 text-gray-400" />
                <span className="text-sm">{profileUser.joinedDate}</span>
            </div>
        </div>
    </div>
}
export default AboutPreview;