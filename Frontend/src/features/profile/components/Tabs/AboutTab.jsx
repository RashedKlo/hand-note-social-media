import { AcademicCapIcon, BriefcaseIcon, EnvelopeIcon, GlobeAltIcon, MapPinIcon, PhoneIcon } from "@heroicons/react/24/outline";

const AboutTab = ({ profileUser }) => {

    return <div className="space-y-4">
        <div className="bg-white rounded-lg shadow-sm p-6">
            <h3 className="text-xl font-semibold text-gray-900 mb-4">About {profileUser?.name}</h3>
            <div className="space-y-6">
                <div>
                    <h4 className="text-lg font-medium text-gray-900 mb-3">Work and Education</h4>
                    <div className="space-y-3">
                        <div className="flex items-center">
                            <BriefcaseIcon className="w-5 h-5 mr-3 text-gray-400" />
                            <div>
                                <p className="text-gray-900">Senior Software Engineer at <strong>Google</strong></p>
                                <p className="text-sm text-gray-500">2020 - Present</p>
                            </div>
                        </div>
                        <div className="flex items-center">
                            <AcademicCapIcon className="w-5 h-5 mr-3 text-gray-400" />
                            <div>
                                <p className="text-gray-900">Studied Computer Science at <strong>Stanford University</strong></p>
                                <p className="text-sm text-gray-500">Graduated 2020</p>
                            </div>
                        </div>
                    </div>
                </div>

                <div>
                    <h4 className="text-lg font-medium text-gray-900 mb-3">Contact and Basic Info</h4>
                    <div className="space-y-3">
                        <div className="flex items-center">
                            <PhoneIcon className="w-5 h-5 mr-3 text-gray-400" />
                            <p className="text-gray-900">+1 (555) 123-4567</p>
                        </div>
                        <div className="flex items-center">
                            <EnvelopeIcon className="w-5 h-5 mr-3 text-gray-400" />
                            <p className="text-gray-900">sarah.johnson@email.com</p>
                        </div>
                        <div className="flex items-center">
                            <MapPinIcon className="w-5 h-5 mr-3 text-gray-400" />
                            <p className="text-gray-900">San Francisco, CA</p>
                        </div>
                        <div className="flex items-center">
                            <GlobeAltIcon className="w-5 h-5 mr-3 text-gray-400" />
                            <p className="text-gray-900">www.sarahjohnson.dev</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
}
export default AboutTab;