import PageContainer from '../components/layout/PageContainer'
import Section from '../components/layout/Section'

function Programmes() {
  return (
    <main>
      <section className="page-hero">
        <PageContainer>
          <div className="page-hero__content">
            <p className="section-eyebrow">Our Programme</p>
            <h1>Full Stack Developer Bootcamp</h1>

            <p>
              A practical, hands-on programme designed to help young people
              build foundational digital and full stack development skills,
              while preparing them for future opportunities.
            </p>
          </div>
        </PageContainer>
      </section>

      <Section>
        <PageContainer>
          <div className="programme-overview">
            <div>
              <h2>Build practical digital skills.</h2>
            </div>

            <div>
              <p>
                The bootcamp takes learners through the foundations of
                computer literacy, development, databases, frontend and
                backend technologies, with AI literacy and career readiness
                built into the learning journey.
              </p>
              <p>
                Learners practise what they learn through hands-on activities
                and projects designed to build confidence and real-world
                experience.
              </p>
            </div>
          </div>
        </PageContainer>
      </Section>

      <Section className="section--light">
        <PageContainer>
          <div className="section-heading">
            <p className="section-eyebrow">What You Will Learn</p>
            <h2>From foundations to building.</h2>
          </div>

          <div className="programme-areas">
            <article className="programme-area">
              <h3>Digital Foundations</h3>
              <p>
                Build the computer literacy, Git and Linux foundations needed
                to work confidently in a development environment.
              </p>
               <span className="programme-area__tech">
                Computer Literacy · Git · Linux
               </span>
            </article>

            <article className="programme-area">
              <h3>Web Development</h3>
              <p>
                Learn how frontend technologies work together to create
                structured, responsive and interactive web experiences.
              </p>
               <span className="programme-area__tech">
                HTML · CSS · JavaScript · React
               </span>
            </article>

            <article className="programme-area">
              <h3>Databases & Backend</h3>
              <p>
                Understand how applications work with data and how backend
                systems support complete digital solutions.
              </p>
               <span className="programme-area__tech">
                SQL · Python
               </span>
            </article>

            <article className="programme-area">
              <h3>AI Literacy</h3>
              <p>
                Develop practical awareness of AI and learn how to use
                prompting effectively as part of modern digital work.
              </p>
               <span className="programme-area__tech">
                AI Tools · Prompt Engineering
                </span>
            </article>

            <article className="programme-area">
              <h3>Career Readiness</h3>
              <p>
                Prepare for opportunities by developing professional
                confidence, workplace awareness and practical career skills.
              </p>
               <span className="programme-area__tech">
                CVs · Applications · Interviews
               </span>
            </article>
          </div>
        </PageContainer>
      </Section>

      <Section>
        <PageContainer>
            <div className="programme-details">
            <div>
                <p className="section-eyebrow">Who This Programme Is For</p>
                <h2>Ready to learn, build and grow.</h2>
            </div>

            <div className="programme-detail-list">
                <div>
                <span>Age</span>
                <strong>18–35</strong>
                </div>

                <div>
                <span>Employment</span>
                <strong>Unemployed youth</strong>
                </div>

                <div>
                <span>Education</span>
                <strong>Matric or higher qualification</strong>
                </div>

                <div>
                <span>Learning</span>
                <strong>Hybrid learning environment</strong>
                </div>

                <div>
                <span>Commitment</span>
                <strong>Dedicated participation throughout the programme</strong>
                </div>
            </div>
            </div>
        </PageContainer>
        </Section>

      <Section className="programme-cta">
        <PageContainer>
          <div className="programme-cta__content">
            <p className="section-eyebrow">Ready to Start?</p>
            <h2>Take your next step with Zenith.</h2>
            <a className="btn btn--primary" href="/apply">
              Apply Now
            </a>
          </div>
        </PageContainer>
      </Section>
    </main>
  )
}

export default Programmes